using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CourseApp.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddBookSellingSystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "book_categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    slug = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_book_categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "book_orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    student_id = table.Column<int>(type: "integer", nullable: true),
                    customer_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    customer_email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    customer_number = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    delivery_address = table.Column<string>(type: "text", nullable: true),
                    total_amount = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    order_status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    payment_status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    paid_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_book_orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    title = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    slug = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    author = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    cover_image_url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    is_physical_available = table.Column<bool>(type: "boolean", nullable: false),
                    physical_price = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    stock_quantity = table.Column<int>(type: "integer", nullable: false),
                    is_pdf_available = table.Column<bool>(type: "boolean", nullable: false),
                    pdf_price = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    pdf_file_url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    is_published = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "book_payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    book_order_id = table.Column<int>(type: "integer", nullable: false),
                    amount = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    payment_method = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    transaction_id = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    paid_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_book_payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_book_payments_book_orders_book_order_id",
                        column: x => x.book_order_id,
                        principalTable: "book_orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "book_category_mappings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    book_id = table.Column<int>(type: "integer", nullable: false),
                    book_category_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_book_category_mappings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_book_category_mappings_book_categories_book_category_id",
                        column: x => x.book_category_id,
                        principalTable: "book_categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_book_category_mappings_books_book_id",
                        column: x => x.book_id,
                        principalTable: "books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "book_order_items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    book_order_id = table.Column<int>(type: "integer", nullable: false),
                    book_id = table.Column<int>(type: "integer", nullable: false),
                    sell_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    unit_price = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    total_price = table.Column<decimal>(type: "numeric(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_book_order_items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_book_order_items_book_orders_book_order_id",
                        column: x => x.book_order_id,
                        principalTable: "book_orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_book_order_items_books_book_id",
                        column: x => x.book_id,
                        principalTable: "books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_book_categories_slug",
                table: "book_categories",
                column: "slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_book_category_mappings_book_category_id",
                table: "book_category_mappings",
                column: "book_category_id");

            migrationBuilder.CreateIndex(
                name: "IX_book_category_mappings_book_id_book_category_id",
                table: "book_category_mappings",
                columns: new[] { "book_id", "book_category_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_book_order_items_book_id",
                table: "book_order_items",
                column: "book_id");

            migrationBuilder.CreateIndex(
                name: "IX_book_order_items_book_order_id",
                table: "book_order_items",
                column: "book_order_id");

            migrationBuilder.CreateIndex(
                name: "IX_book_payments_book_order_id",
                table: "book_payments",
                column: "book_order_id");

            migrationBuilder.CreateIndex(
                name: "IX_book_payments_transaction_id",
                table: "book_payments",
                column: "transaction_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_books_slug",
                table: "books",
                column: "slug",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "book_category_mappings");

            migrationBuilder.DropTable(
                name: "book_order_items");

            migrationBuilder.DropTable(
                name: "book_payments");

            migrationBuilder.DropTable(
                name: "book_categories");

            migrationBuilder.DropTable(
                name: "books");

            migrationBuilder.DropTable(
                name: "book_orders");
        }
    }
}
