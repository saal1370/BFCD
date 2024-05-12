using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BFCD.Server.Domain
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }
        public DateTime TransactionDate { get; set; }
        public Decimal TransactionAmount { get; set; }
        public Decimal NewBalance { get; set; }

        public Transaction(DateTime transactionDate, decimal transactionAmount, decimal newBalance)
        {
            TransactionDate = transactionDate;
            TransactionAmount = transactionAmount;
            NewBalance = newBalance;
        }
    }
}
