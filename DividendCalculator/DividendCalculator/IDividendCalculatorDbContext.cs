using DividendCalculator.Models;
using Microsoft.EntityFrameworkCore;

namespace DividendCalculator
{
    public interface IDividendCalculatorDbContext
    {
        DbSet<Transaction> Transactions { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
