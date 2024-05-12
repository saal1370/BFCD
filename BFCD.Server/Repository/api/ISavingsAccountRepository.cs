
using BFCD.Server.Domain;

public interface ISavingsAccountRepository
{
    SavingsAccount CreateSavingsAccount(int customerId, SavingsAccount savingsAccount);
    Transaction DepositToSavingsAccount(int customerId, String accountName, decimal amount);
    Transaction WidthdrowFromSavingsAccount(int customerId, String accountName, decimal amount);
    IEnumerable<Transaction> GetLastTenTransactions(int customerId, String accountName);
}