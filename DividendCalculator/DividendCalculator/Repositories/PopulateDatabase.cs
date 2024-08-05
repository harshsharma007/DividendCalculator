using DividendCalculator.Models;

namespace DividendCalculator.Repositories;

public class PopulateDatabase(IDividendCalculatorDbContext dbContext) : IPopulateDatabase
{
    private readonly IDividendCalculatorDbContext _dbContext = dbContext;

    public async Task ExecuteAsync(List<Transaction> transactions)
    {
        try
        {
            foreach (var transaction in transactions)
            {
                if (!_dbContext.Transactions.Any(t =>
                    t.TransactionDate.Date == transaction.TransactionDate.Date &&
                    t.ValueDate.Date == transaction.ValueDate.Date &&
                    t.Balance == transaction.Balance &&
                    t.ChequeNumber!.Trim() == transaction.ChequeNumber!.Trim() &&
                    t.TransactionRemarks.Trim() == transaction.TransactionRemarks.Trim() &&
                    t.WithdrawalAmount == transaction.WithdrawalAmount &&
                    t.DepositAmount == transaction.DepositAmount))
                {
                    _dbContext.Transactions.AddRange(transaction);
                }
            }
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            throw;
        }
    }
}
