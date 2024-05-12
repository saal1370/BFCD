
using BFCD.Server.Domain;

public interface ITransactionRepository
{
    List<Transaction> GetLastTen();
    Transaction CreateTransaction(decimal amount, SavingsAccount savingsAccount);
}