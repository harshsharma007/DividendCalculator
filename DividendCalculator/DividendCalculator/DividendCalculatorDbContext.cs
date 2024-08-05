using DividendCalculator.Models;
using Microsoft.EntityFrameworkCore;

namespace DividendCalculator;

public class DividendCalculatorDbContext(DbContextOptions<DividendCalculatorDbContext> options) : DbContext(options), IDividendCalculatorDbContext
{
    public DbSet<Transaction> Transactions { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Transaction>()
        .HasKey(t => new {
            t.TransactionDate,
            t.ValueDate,
            t.Balance,
            t.ChequeNumber,
            t.TransactionRemarks,
            t.WithdrawalAmount,
            t.DepositAmount
        });
    }
}
