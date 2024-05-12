
using BFCD.Server.Domain;
using Moq;

[TestFixture]
public class InMemorySavingsAccountRepTests
{
    private InMemorySavingsAccountRep savingsAccountRepository;
    private Mock<ICustomerRepository> mockCustomerRepository;
    private Mock<ITransactionRepository> mockTransactionRepository;

    [SetUp]
    public void Setup()
    {
        // Mocking dependencies
        mockCustomerRepository = new Mock<ICustomerRepository>();
        mockTransactionRepository = new Mock<ITransactionRepository>();

        // Initializing the repository with mocks
        savingsAccountRepository = new InMemorySavingsAccountRep(new List<SavingsAccount>(), mockCustomerRepository.Object, mockTransactionRepository.Object);
    }

    [Test]
    public void CreateSavingsAccount_ReturnsSavingsAccount_WhenCustomerExists()
    {
        // Arrange
        var customer = new Customer("UpdatedName", "UpdatedLastName", new DateTime(1990, 1, 1));
        customer.CustomerId = 1;
        customer.SavingsAccounts = new List<SavingsAccount>();
        var savingsAccount = new SavingsAccount(1, "TestAccount", 100, null);
        mockCustomerRepository.Setup(c => c.GetById(1)).Returns(customer);
        mockTransactionRepository.Setup(t => t.CreateTransaction(It.IsAny<decimal>(), It.IsAny<decimal>(), It.IsAny<SavingsAccount>())).Returns(new Transaction(new DateTime(2024, 1, 1), 65, 65));

        // Act
        var result = savingsAccountRepository.CreateSavingsAccount(1, savingsAccount);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.AccountId);
        Assert.AreEqual(1, customer.SavingsAccounts.Count);
    }

    [Test]
    public void WidthdrowFromSavingsAccount_ThrowsException_WhenAccountDoesNotExist()
    {
        // Arrange
        var customerId = 1;
        var accountName = "NonExistingAccount";
        var customer = new Customer("UpdatedName", "UpdatedLastName", new DateTime(1990, 1, 1));
        customer.CustomerId = 1;
        var savingsAccount = new SavingsAccount(1, accountName, 30, null);
   
        customer.SavingsAccounts = new List<SavingsAccount>() { savingsAccount };
        mockCustomerRepository.Setup(c => c.GetById(customerId)).Returns(customer);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => savingsAccountRepository.WidthdrowFromSavingsAccount(customerId, accountName, 100));
    }

    [Test]
    public void DepositToSavingsAccount_IncreasesBalance_WhenAccountExists()
    {
        // Arrange
        var accountName = "TestAccount";
        var customer = new Customer("UpdatedName", "UpdatedLastName", new DateTime(1990, 1, 1));
        customer.CustomerId = 1;
        customer.SavingsAccounts = new List<SavingsAccount> { new SavingsAccount(1, accountName, 100, new List<Transaction>())};
        mockCustomerRepository.Setup(c => c.GetById(1)).Returns(customer);
        mockTransactionRepository.Setup(t => t.CreateTransaction(It.IsAny<decimal>(), It.IsAny<decimal>(), It.IsAny<SavingsAccount>())).Returns(new Transaction(new DateTime(2024, 1, 1), 65, 65));

        // Act
        var result = savingsAccountRepository.DepositToSavingsAccount(1, accountName, 50);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(150, customer.SavingsAccounts.First().Balance);
    }

    [Test]
    public void GetLastTenTransactions_ReturnsLastTenTransactions_WhenAccountExists()
    {
        // Arrange
        var customer = new Customer("UpdatedName", "UpdatedLastName", new DateTime(1990, 1, 1));
        customer.CustomerId = 1;
        var accountName = "TestAccount";
        customer.SavingsAccounts = new List<SavingsAccount> {new SavingsAccount(1, accountName, 100, new List<Transaction>()) };


        for (int i = 1; i <= 15; i++)
        {
            var transaction = new Transaction(new DateTime(2024, 1, i), 10 * i, 10 * i);
            transaction.TransactionId = i;
            customer.SavingsAccounts.First().Transactions.Add(transaction);
        }
        mockCustomerRepository.Setup(c => c.GetById(1)).Returns(customer);

        // Act
        var result = savingsAccountRepository.GetLastTenTransactions(1, accountName);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(10, result.Count());
        Assert.AreEqual(15, result.First().TransactionId);
        Assert.AreEqual(6, result.Last().TransactionId);
    }

    // Add more tests for other methods as needed...
}