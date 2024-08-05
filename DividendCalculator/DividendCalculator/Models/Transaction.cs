using System.ComponentModel.DataAnnotations.Schema;

namespace DividendCalculator.Models;

public class Transaction
{
    [NotMapped]
    public int SNo { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }
    public DateTime ValueDate { get; set; }
    public DateTime TransactionDate { get; set; }
    public string? ChequeNumber { get; set; }
    public string TransactionRemarks { get; set; } = string.Empty;
    public decimal? WithdrawalAmount { get; set; }
    public decimal? DepositAmount { get; set; }
    public decimal Balance { get; set; }
}
