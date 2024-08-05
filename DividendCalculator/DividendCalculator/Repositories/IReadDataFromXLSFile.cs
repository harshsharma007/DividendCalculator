using DividendCalculator.Models;

namespace DividendCalculator.Repositories;

public interface IReadDataFromXLSFile
{
    List<Transaction> Invoke(string filePath);
}
