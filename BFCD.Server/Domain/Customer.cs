using Newtonsoft.Json;
using NSwag.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BFCD.Server.Domain
{
    public class Customer
    {
        private readonly List<SavingsAccount> SavingsAccount;

        [Key] 
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdag { get; set; }

        [ForeignKey(nameof(SavingsAccount))]
        [JsonIgnore]
        public List<SavingsAccount> SavingsAccounts { get; set; } = new List<SavingsAccount>();
    }
}
