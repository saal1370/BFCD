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
        public SavingsAccount SavingsAccount { get; set; }

        public Transaction(int transactionId, DateTime transactionDate, decimal transactionAmount, SavingsAccount savingsAccount)
        {
            TransactionId = transactionId;
            TransactionDate = transactionDate;
            TransactionAmount = transactionAmount;
            NewBalance = savingsAccount.Balance;
        }
    }
}
