using System.ComponentModel.DataAnnotations.Schema;

namespace BFCD.Server.Domain
{
    public class SavingsAcount
    {
        public int CustomerId { get; set; }
        public required string Name { get; set; }
        public required string LastName { get; set; }
        public DateTime Birthdag { get; set; }

        // Navigation property to SavingsAccount
        //public SavingsAccount? SavingsAccount { get; set; }

        // Foreign key to SavingsAccount (optional, can also be inferred)
        //[ForeignKey("SavingsAccount")]
        //public int SavingsAccountId { get; set; }
    }
}
