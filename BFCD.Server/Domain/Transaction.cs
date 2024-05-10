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

        [ForeignKey(nameof(SavingsAccount))]
        public SavingsAccount SavingsAccount { get; set; }

        [ForeignKey(nameof(Customer))]
        public Customer Customer { get; set; }

        public Transaction(int transactionId, DateTime transactionDate, decimal transactionAmount, Customer customer, SavingsAccount savingsAccount)
        {
            TransactionId = transactionId;
            TransactionDate = transactionDate;
            TransactionAmount = transactionAmount;
            Customer = customer;
            SavingsAccount = savingsAccount;
        }
        public Transaction() { }
    }
}
