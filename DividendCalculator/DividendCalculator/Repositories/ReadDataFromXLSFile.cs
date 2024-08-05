using DividendCalculator.Models;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Globalization;

namespace DividendCalculator.Repositories;

public class ReadDataFromXLSFile : IReadDataFromXLSFile
{
    public List<Transaction> Invoke(string filePath)
    {
        var transactions = new List<Transaction>();

        using var file = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        var workbook = new HSSFWorkbook(file);
        ISheet sheet = workbook.GetSheetAt(0);

        for (int row = 2; row <= sheet.LastRowNum; row++)
        {
            IRow currentRow = sheet.GetRow(row);
            if (currentRow == null || currentRow.GetCell(0).ToString()?.Trim() == "") continue;

            var transaction = new Transaction
            {
                SNo = Convert.ToInt32(currentRow.GetCell(0).ToString()?.Trim()),
                ValueDate = DateTime.ParseExact(currentRow.GetCell(1).ToString()!.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture),
                TransactionDate = DateTime.ParseExact(currentRow.GetCell(2).ToString()!.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture),
                ChequeNumber = currentRow.GetCell(3).ToString()?.Trim(),
                TransactionRemarks = currentRow.GetCell(4).ToString()!.Trim(),
                WithdrawalAmount = string.IsNullOrEmpty(currentRow.GetCell(5).ToString()) ? (decimal?)null : decimal.Parse(currentRow.GetCell(5).ToString()!.Trim()),
                DepositAmount = string.IsNullOrEmpty(currentRow.GetCell(6).ToString()) ? (decimal?)null : decimal.Parse(currentRow.GetCell(6).ToString()!.Trim()),
                Balance = decimal.Parse(currentRow.GetCell(7).ToString()!.Trim())
            };
            transactions.Add(transaction);
        }

        return transactions;
    }
}
