
using BFCD.Server.Domain;

public class InMemorySavingsAccountRep : ISavingsAccountRepository
{
    private readonly List<SavingsAccount> savingsAccounts = new();
    private readonly ICustomerRepository customerRepository;
    public void CreateSavingsAccount(int customerId, SavingsAccount savingsAccount)
    {
        if (savingsAccounts.Any())
            savingsAccount.AccountId = savingsAccounts.Max(sa => sa.AccountId) + 1;
        else
            savingsAccount.AccountId = 1;

        var customer = customerRepository.GetById(customerId);
        savingsAccount.Customer = customer;
        customer.SavingsAccount = savingsAccount;

        savingsAccounts.Add(savingsAccount);
    }

    public decimal DepositToSavingsAccount(SavingsAccount savingsAccount)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Customer> GetAll()
    {
        throw new NotImplementedException();
    }

    public decimal GetBalanceById(int id)
    {
        throw new NotImplementedException();
    }

    public List<SavingsAccount> GetByCustomerId(int customerId)
    {
        throw new NotImplementedException();
    }

    public SavingsAccount GetById(int id)
    {
        throw new NotImplementedException();
    }

    public SavingsAccount GetByName(string name)
    {
        throw new NotImplementedException();
    }

    public decimal WidthdrowFromSavingsAccount(SavingsAccount savingsAccount)
    {
        throw new NotImplementedException();
    }
}
