using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CourseApp.Core.Entities;

namespace CourseApp.Application.Interfaces;

public interface IPaymentRepository
{
    Task AddAsync(Payment payment, CancellationToken ct = default);
    Task<Payment?> GetByTransactionIdAsync(string trxId, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
}