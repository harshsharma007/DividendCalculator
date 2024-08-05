using DividendCalculator.Models;
using DividendCalculator.Repositories;
using Microsoft.Extensions.Configuration;

namespace DividendCalculator.Services;

public class InitiateAppService(IReadDataFromXLSFile readDataFromXLSFile, IConfiguration configuration, IPopulateDatabase populateDatabase) : IInitiateAppService
{
    private readonly IConfiguration _configuration = configuration;
    private readonly IReadDataFromXLSFile _readDataFromXLSFile = readDataFromXLSFile;
    private readonly IPopulateDatabase _populateDatabase = populateDatabase;

    public async Task ExecuteAsync()
    {
        var completeData = new List<Transaction>();

        var files = Directory.GetFiles(_configuration["FolderPath"]!);
        foreach (var file in files)
        {
            completeData.AddRange(_readDataFromXLSFile.Invoke(file));
        }

        await _populateDatabase.ExecuteAsync([.. completeData.OrderBy(x => x.TransactionDate)
            .DistinctBy(x => new
            {
                x.ChequeNumber,
                x.ValueDate,
                x.TransactionDate,
                x.Balance,
                x.DepositAmount,
                x.WithdrawalAmount,
                x.TransactionRemarks
            })]);
    }
}
