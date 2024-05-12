
using BFCD.Server.Domain;

public class InMemoryTransactionRep : ITransactionRepository
{
    private readonly List<Transaction> transactions = new();

    private int SetTransactionID(List<Transaction> transactions)
    {
        if (transactions.Any())
            return transactions.Max(t => t.TransactionId) + 1;
        else
            return 1;
    }

    public Transaction CreateTransaction(decimal amount, decimal newBalance, SavingsAccount savingsAccount)
    {
        var transaction = new Transaction(DateTime.Now, amount, newBalance);
        transaction.TransactionId = SetTransactionID(savingsAccount.Transactions); ;

        return transaction;
    }

    List<Transaction> ITransactionRepository.GetLastTen()
    {
        throw new NotImplementedException();
    }
    
    
}
