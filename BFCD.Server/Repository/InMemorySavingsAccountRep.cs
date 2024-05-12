
using BFCD.Server.Domain;
using Microsoft.IdentityModel.Tokens;

public class InMemorySavingsAccountRep : ISavingsAccountRepository
{
    private readonly List<SavingsAccount> savingsAccounts;
    private readonly ICustomerRepository customerRepository;
    private readonly ITransactionRepository transactionRepository;

    public InMemorySavingsAccountRep(List<SavingsAccount> savingsAccounts, ICustomerRepository customerRepository, ITransactionRepository transactionRepository)
    {
        this.savingsAccounts = savingsAccounts;
        this.customerRepository = customerRepository;
        this.transactionRepository = transactionRepository;
    }

    public SavingsAccount CreateSavingsAccount(int customerId, SavingsAccount savingsAccount)
    {
        var customer = customerRepository.GetById(customerId);
        var transaction = transactionRepository.CreateTransaction(savingsAccount.Balance, savingsAccount.Balance, savingsAccount);
        var transactions = new List<Transaction>();
        transactions.Add(transaction);

        savingsAccount.AccountId = SetSavingsAccountId();
        savingsAccount.Transactions = transactions;

        if (customer.SavingsAccounts.IsNullOrEmpty()) 
        {
            var newSavingsAccounts = new List<SavingsAccount>();
            newSavingsAccounts.Add(savingsAccount);
            customer.SavingsAccounts = newSavingsAccounts;
        } else
            customer.SavingsAccounts.Add(savingsAccount);

        savingsAccounts.Add(savingsAccount);
        return savingsAccount;
    }

    private int SetSavingsAccountId() 
    {
        if (savingsAccounts.Any())
            return savingsAccounts.Max(sa => sa.AccountId) + 1;
        else
            return 1;
    }

    public Transaction WidthdrowFromSavingsAccount(int customerId, String accountName, decimal amount)
    {
        var customer = customerRepository.GetById(customerId);
        var savingsAccount = customer.SavingsAccounts.FirstOrDefault(sa => sa.AcountName.Equals(accountName));

        var newBalance = savingsAccount.Balance - amount;
        if (newBalance < 0)
            throw new InvalidOperationException($"Account {accountName} can not be withdrowed by {amount} amount, since negative balance is not allowed");
        savingsAccount.Balance = newBalance;

        var transaction = transactionRepository.CreateTransaction((amount * -1), savingsAccount.Balance, savingsAccount);
        savingsAccount.Transactions.Add(transaction);
        
        return transaction;
    }

    public IEnumerable<Transaction> GetLastTenTransactions(int customerId, String accountName)
    {
        var customer = customerRepository.GetById(customerId);
        var savingsAccount = customer.SavingsAccounts.FirstOrDefault(sa => sa.AcountName.Equals(accountName));

        var lastTenTransactions = savingsAccount.Transactions.OrderByDescending(t => t.TransactionDate).Take(10).ToList();
        return lastTenTransactions;
    }

    public Transaction DepositToSavingsAccount(int customerId, String accountName, decimal amount)
    {
        var customer = customerRepository.GetById(customerId);
        var savingsAccount = customer.SavingsAccounts.FirstOrDefault(sa => sa.AcountName.Equals(accountName));

        var newBalance = savingsAccount.Balance + amount;
        savingsAccount.Balance = newBalance;

        var transaction = transactionRepository.CreateTransaction(amount, savingsAccount.Balance, savingsAccount);
        savingsAccount.Transactions.Add(transaction);

        return transaction;
    }
}
