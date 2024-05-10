using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BFCD.Server.Domain
{
    public class Customer
    {
        [Key] 
        public int CustomerId { get; set; }
        public required string Name { get; set; }
        public required string LastName { get; set; }
        public DateTime Birthdag { get; set; }

        [ForeignKey(nameof(SavingsAccount))]
        public SavingsAccount? SavingsAccount { get; set; } 

        [ForeignKey(nameof(Transaction))]
        public HashSet<Transaction>? Transactions { get; set; } = new();
    }
}
