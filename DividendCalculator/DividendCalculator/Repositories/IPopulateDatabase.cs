using DividendCalculator.Models;

namespace DividendCalculator.Repositories;

public interface IPopulateDatabase
{
    Task ExecuteAsync(List<Transaction> transactions);
}
