
using BFCD.Server.Domain;

public interface ITransactionRepository
{
    List<Transaction> GetLastTen();
    Transaction CreateTransaction(decimal amount, decimal newBalance, SavingsAccount savingsAccount);
}