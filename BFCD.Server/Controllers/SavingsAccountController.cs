using BFCD.Server.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BFCD.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SavingsAccountController : ControllerBase
    {
        private readonly ISavingsAccountRepository _savingsAccountRep;

        // Constructor accepting the DbContext as a dependency
        public SavingsAccountController(ISavingsAccountRepository savingsAccountRep)
        {
            _savingsAccountRep = savingsAccountRep;
        }

        [HttpPost("CreateSavingsAccount")]
        public IActionResult CreateSavingsAccount(int customerId, [FromBody] SavingsAccount savingsAccount)
        {
            var newSavingsAccount = _savingsAccountRep.CreateSavingsAccount(customerId, savingsAccount);
            return Created(nameof(CreateSavingsAccount), newSavingsAccount);
        }

        [HttpGet("GetLastTenTransactions")]
        public IActionResult GetLastTenTransactions(int customerId, String accountName)
        {
            var transactions = _savingsAccountRep.GetLastTenTransactions(customerId, accountName);
            return Ok(transactions);
        }

        [HttpPost("DepositeToSavingAccount")]
        public IActionResult UpdateCustomer(int customerId, String accountName, decimal amount)
        {
            var transaction = _savingsAccountRep.DepositToSavingsAccount(customerId, accountName, amount);
            return Ok(transaction);
        }

        [HttpPost("WithdrowFromSavingsAccount")]
        public IActionResult WithdrowFromSavingsAccount(int customerId, String accountName, decimal amount)
        {
            var transaction = _savingsAccountRep.WidthdrowFromSavingsAccount(customerId, accountName, amount);

            // Return the unique updated customer
            return Ok(transaction);
        }

    }
}