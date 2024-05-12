using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BFCD.Server.Domain
{
    public class SavingsAccount
    {
        [Key]
        public int AccountId { get; set; }
        public string AcountName { get; set; }
        public Decimal Balance { get; set; }

        [ForeignKey(nameof(Transaction))]
        public List<Transaction> Transactions { get; set; }
    }
}
