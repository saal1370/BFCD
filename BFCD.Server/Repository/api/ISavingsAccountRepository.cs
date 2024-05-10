
using BFCD.Server.Domain;

public interface ISavingsAccountRepository
{
    IEnumerable<Customer> GetAll();
    SavingsAccount GetById(int id);
    SavingsAccount GetByName(string name);
    List<SavingsAccount> GetByCustomerId(int customerId);
    void CreateSavingsAccount(int customerId, SavingsAccount savingsAccount);
    Decimal GetBalanceById(int id);
    Decimal DepositToSavingsAccount(SavingsAccount savingsAccount);
    Decimal WidthdrowFromSavingsAccount(SavingsAccount savingsAccount);
}