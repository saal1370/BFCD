
using BFCD.Server.Domain;

public class InMemoryTransactionRep : ITransactionRepository
{
    private readonly List<Transaction> transactions = new();

    private int SetTransactionID()
    {
        if (transactions.Any())
            return transactions.Max(t => t.TransactionId) + 1;
        else
            return 1;
    }

    public Transaction CreateTransaction(decimal amount, SavingsAccount savingsAccount)
    {
        int newTransactionId = SetTransactionID();
        var transaction = new Transaction(newTransactionId, DateTime.Now, amount, savingsAccount);

        return transaction;
    }

    List<Transaction> ITransactionRepository.GetLastTen()
    {
        throw new NotImplementedException();
    }
    
    
}
