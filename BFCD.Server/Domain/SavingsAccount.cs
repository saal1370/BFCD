using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BFCD.Server.Domain
{
    public class SavingsAccount
    {
        [Key]
        public int AccountId { get; set; }
        public required string AcountName { get; set; }
        public required Decimal Balance { get; set; }

        [ForeignKey(nameof(Customer))]
        public required Customer Customer { get; set; }

        [ForeignKey(nameof(Transaction))]
        public required HashSet<Transaction> Transactions { get; set; }
    }
}
