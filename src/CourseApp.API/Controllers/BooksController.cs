using CourseApp.Application.DTOs.Books;
using CourseApp.Core.Entities;
using CourseApp.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CourseApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public BooksController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost("categories")]
    public async Task<IActionResult> CreateCategory(
        [FromBody] CreateBookCategoryRequest request,
        CancellationToken cancellationToken)
    {
        var exists = await _dbContext.BookCategories
            .AnyAsync(x => x.Slug == request.Slug, cancellationToken);

        if (exists)
        {
            return BadRequest(new { message = "Category slug already exists." });
        }

        var category = new BookCategory
        {
            Name = request.Name,
            Slug = request.Slug,
            Description = request.Description,
            CreatedAt = DateTime.UtcNow
        };

        await _dbContext.BookCategories.AddAsync(category, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Ok(category);
    }

    [HttpGet("categories")]
    public async Task<IActionResult> GetCategories(CancellationToken cancellationToken)
    {
        var data = await _dbContext.BookCategories
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .ToListAsync(cancellationToken);

        return Ok(data);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBook(
        [FromBody] CreateBookRequest request,
        CancellationToken cancellationToken)
    {
        if (request.BookCategoryIds.Count == 0)
        {
            return BadRequest(new { message = "At least one category is required." });
        }

        var exists = await _dbContext.Books
            .AnyAsync(x => x.Slug == request.Slug, cancellationToken);

        if (exists)
        {
            return BadRequest(new { message = "Book slug already exists." });
        }

        var categories = await _dbContext.BookCategories
            .Where(x => request.BookCategoryIds.Contains(x.Id))
            .ToListAsync(cancellationToken);

        if (categories.Count != request.BookCategoryIds.Distinct().Count())
        {
            return BadRequest(new { message = "One or more book categories are invalid." });
        }

        if (!request.IsPhysicalAvailable && !request.IsPdfAvailable)
        {
            return BadRequest(new { message = "At least one sell type must be available." });
        }

        if (request.IsPhysicalAvailable && request.PhysicalPrice <= 0)
        {
            return BadRequest(new { message = "Physical price must be greater than 0." });
        }

        if (request.IsPdfAvailable)
        {
            if (request.PdfPrice is null || request.PdfPrice <= 0)
            {
                return BadRequest(new { message = "PDF price must be greater than 0." });
            }

            if (string.IsNullOrWhiteSpace(request.PdfFileUrl))
            {
                return BadRequest(new { message = "PDF file URL is required." });
            }
        }

        var book = new Book
        {
            Title = request.Title,
            Slug = request.Slug,
            Author = request.Author,
            Description = request.Description,
            CoverImageUrl = request.CoverImageUrl,
            IsPhysicalAvailable = request.IsPhysicalAvailable,
            PhysicalPrice = request.PhysicalPrice,
            StockQuantity = request.StockQuantity,
            IsPdfAvailable = request.IsPdfAvailable,
            PdfPrice = request.PdfPrice,
            PdfFileUrl = request.PdfFileUrl,
            IsPublished = request.IsPublished,
            CreatedAt = DateTime.UtcNow,
            BookCategoryMappings = request.BookCategoryIds
                .Distinct()
                .Select(categoryId => new BookCategoryMapping
                {
                    BookCategoryId = categoryId
                })
                .ToList()
        };

        await _dbContext.Books.AddAsync(book, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Ok(new
        {
            message = "Book created successfully.",
            book.Id,
            book.Title,
            categories = categories.Select(x => new { x.Id, x.Name })
        });
    }

    [HttpGet]
    public async Task<IActionResult> GetBooks(CancellationToken cancellationToken)
    {
        var data = await _dbContext.Books
            .Include(x => x.BookCategoryMappings)
                .ThenInclude(x => x.BookCategory)
            .AsNoTracking()
            .OrderByDescending(x => x.Id)
            .Select(x => new
            {
                x.Id,
                x.Title,
                x.Slug,
                x.Author,
                x.Description,
                x.CoverImageUrl,
                x.IsPhysicalAvailable,
                x.PhysicalPrice,
                x.StockQuantity,
                x.IsPdfAvailable,
                x.PdfPrice,
                x.PdfFileUrl,
                x.IsPublished,
                x.CreatedAt,
                Categories = x.BookCategoryMappings.Select(c => new
                {
                    c.BookCategory.Id,
                    c.BookCategory.Name,
                    c.BookCategory.Slug
                })
            })
            .ToListAsync(cancellationToken);

        return Ok(data);
    }

    [HttpGet("category/{categoryId:int}")]
    public async Task<IActionResult> GetBooksByCategory(
        int categoryId,
        CancellationToken cancellationToken)
    {
        var data = await _dbContext.Books
            .Include(x => x.BookCategoryMappings)
                .ThenInclude(x => x.BookCategory)
            .AsNoTracking()
            .Where(x => x.BookCategoryMappings.Any(c => c.BookCategoryId == categoryId))
            .OrderByDescending(x => x.Id)
            .Select(x => new
            {
                x.Id,
                x.Title,
                x.Slug,
                x.Author,
                x.Description,
                x.CoverImageUrl,
                x.IsPhysicalAvailable,
                x.PhysicalPrice,
                x.StockQuantity,
                x.IsPdfAvailable,
                x.PdfPrice,
                x.PdfFileUrl,
                x.IsPublished,
                x.CreatedAt,
                Categories = x.BookCategoryMappings.Select(c => new
                {
                    c.BookCategory.Id,
                    c.BookCategory.Name,
                    c.BookCategory.Slug
                })
            })
            .ToListAsync(cancellationToken);

        return Ok(data);
    }

    [HttpPost("orders")]
    public async Task<IActionResult> CreateOrder(
        [FromBody] CreateBookOrderRequest request,
        CancellationToken cancellationToken)
    {
        if (request.Items.Count == 0)
        {
            return BadRequest(new { message = "At least one item is required." });
        }

        var order = new BookOrder
        {
            StudentId = request.StudentId,
            CustomerName = request.CustomerName,
            CustomerEmail = request.CustomerEmail,
            CustomerNumber = request.CustomerNumber,
            DeliveryAddress = request.DeliveryAddress,
            OrderStatus = "Pending",
            PaymentStatus = "Unpaid",
            CreatedAt = DateTime.UtcNow
        };

        decimal totalAmount = 0;

        foreach (var item in request.Items)
        {
            var book = await _dbContext.Books
                .FirstOrDefaultAsync(x => x.Id == item.BookId && x.IsPublished, cancellationToken);

            if (book is null)
            {
                return NotFound(new { message = $"Book not found. BookId: {item.BookId}" });
            }

            var sellType = item.SellType.Trim();

            if (sellType != "Physical" && sellType != "Pdf")
            {
                return BadRequest(new { message = "SellType must be Physical or Pdf." });
            }

            if (sellType == "Physical")
            {
                if (!book.IsPhysicalAvailable)
                {
                    return BadRequest(new { message = $"{book.Title} physical book is not available." });
                }

                if (item.Quantity <= 0)
                {
                    return BadRequest(new { message = "Quantity must be greater than 0." });
                }

                if (book.StockQuantity < item.Quantity)
                {
                    return BadRequest(new { message = $"{book.Title} stock not enough." });
                }

                var itemTotal = book.PhysicalPrice * item.Quantity;

                order.Items.Add(new BookOrderItem
                {
                    BookId = book.Id,
                    SellType = "Physical",
                    Quantity = item.Quantity,
                    UnitPrice = book.PhysicalPrice,
                    TotalPrice = itemTotal
                });

                totalAmount += itemTotal;
            }

            if (sellType == "Pdf")
            {
                if (!book.IsPdfAvailable || book.PdfPrice is null)
                {
                    return BadRequest(new { message = $"{book.Title} PDF is not available." });
                }

                var itemTotal = book.PdfPrice.Value;

                order.Items.Add(new BookOrderItem
                {
                    BookId = book.Id,
                    SellType = "Pdf",
                    Quantity = 1,
                    UnitPrice = book.PdfPrice.Value,
                    TotalPrice = itemTotal
                });

                totalAmount += itemTotal;
            }
        }

        order.TotalAmount = totalAmount;

        await _dbContext.BookOrders.AddAsync(order, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Ok(new
        {
            message = "Book order created successfully.",
            order.Id,
            order.TotalAmount,
            order.PaymentStatus,
            order.OrderStatus
        });
    }

    [HttpPost("payments")]
    public async Task<IActionResult> CreatePayment(
        [FromBody] CreateBookPaymentRequest request,
        CancellationToken cancellationToken)
    {
        var order = await _dbContext.BookOrders
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.Id == request.BookOrderId, cancellationToken);

        if (order is null)
        {
            return NotFound(new { message = "Book order not found." });
        }

        if (order.PaymentStatus == "Paid")
        {
            return BadRequest(new { message = "Order already paid." });
        }

        var transactionExists = await _dbContext.BookPayments
            .AnyAsync(x => x.TransactionId == request.TransactionId, cancellationToken);

        if (transactionExists)
        {
            return BadRequest(new { message = "Transaction ID already exists." });
        }

        var payment = new BookPayment
        {
            BookOrderId = order.Id,
            Amount = order.TotalAmount,
            PaymentMethod = request.PaymentMethod,
            TransactionId = request.TransactionId,
            Status = "Success",
            CreatedAt = DateTime.UtcNow,
            PaidAt = DateTime.UtcNow
        };

        order.PaymentStatus = "Paid";
        order.OrderStatus = "Processing";
        order.PaidAt = DateTime.UtcNow;

        foreach (var item in order.Items.Where(x => x.SellType == "Physical"))
        {
            var book = await _dbContext.Books
                .FirstAsync(x => x.Id == item.BookId, cancellationToken);

            book.StockQuantity -= item.Quantity;
            book.UpdatedAt = DateTime.UtcNow;
        }

        await _dbContext.BookPayments.AddAsync(payment, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Ok(new
        {
            message = "Payment successful.",
            payment.Id,
            payment.Amount,
            order.PaymentStatus,
            order.OrderStatus
        });
    }

    [HttpGet("orders/{orderId:int}")]
    public async Task<IActionResult> GetOrder(
        int orderId,
        CancellationToken cancellationToken)
    {
        var order = await _dbContext.BookOrders
            .Include(x => x.Items)
                .ThenInclude(x => x.Book)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == orderId, cancellationToken);

        if (order is null)
        {
            return NotFound(new { message = "Order not found." });
        }

        return Ok(order);
    }
}