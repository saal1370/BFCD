
using BFCD.Server.Domain;

public interface ITransactionRepository
{
    List<Transaction> GetLastTen();
    Transaction CreateTransaction(decimal amount, Customer customer, SavingsAccount savingsAccount);
}