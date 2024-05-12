using BFCD.Server.Domain;
using Microsoft.AspNetCore.Http;

[TestFixture]
public class InMemoryCustomerRepTests
{
    private InMemoryCustomerRep customerRepository;

    [SetUp]
    public void Setup()
    {
        // Initialize the repository for each test
        customerRepository = new InMemoryCustomerRep();
    }

    [Test]
    public void GetAll_ReturnsEmptyList_WhenNoCustomersAdded()
    {
        // Act
        var result = customerRepository.GetAll();

        // Assert
        Assert.IsEmpty(result);
    }

    [Test]
    public void GetById_ReturnsCorrectCustomer_WhenCustomerExists()
    {
        // Arrange
        var customer = customerRepository.Add("John", "Doe", new DateTime(1990, 1, 1));

        // Act
        var result = customerRepository.GetById(customer.CustomerId);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(customer, result);
    }

    [Test]
    public void GetById_ThrowsException_WhenCustomerDoesNotExist()
    {
        // Act & Assert
        Assert.Throws<BadHttpRequestException>(() => customerRepository.GetById(1));
    }

    [Test]
    public void Add_IncrementsCustomerId_WhenCustomersExist()
    {
        // Arrange
        customerRepository.Add("John", "Doe", new DateTime(1990, 1, 1));

        // Act
        var result = customerRepository.Add("Jane", "Doe", new DateTime(1995, 5, 5));

        // Assert
        Assert.AreEqual(2, result.CustomerId);
    }

    [Test]
    public void Add_SetsCustomerIdToOne_WhenFirstCustomerAdded()
    {
        // Act
        var result = customerRepository.Add("John", "Doe", new DateTime(1990, 1, 1));

        // Assert
        Assert.AreEqual(1, result.CustomerId);
    }

    [Test]
    public void Update_UpdatesCustomer_WhenCustomerExists()
    {
        // Arrange
        var customer = customerRepository.Add("John", "Doe", new DateTime(1990, 1, 1));
        var updatedCustomer = new Customer ("UpdatedName", "UpdatedLastName", new DateTime(1990, 1, 1));
        updatedCustomer.CustomerId = 1;

        // Act
        customerRepository.Update(updatedCustomer);
        var result = customerRepository.GetById(customer.CustomerId);

        // Assert
        Assert.AreEqual(updatedCustomer.Name, result.Name);
        Assert.AreEqual(updatedCustomer.LastName, result.LastName);
        Assert.AreEqual(updatedCustomer.Birthdag, result.Birthdag);
    }

    [Test]
    public void Update_ThrowsException_WhenCustomerDoesNotExist()
    {
        // Arrange
        var nonExistingCustomer = new Customer("NewCustomer", "NewLastName", new DateTime(2000, 1, 1));

        // Act & Assert
        Assert.Throws<BadHttpRequestException>(() => customerRepository.Update(nonExistingCustomer));
    }

    [Test]
    public void Delete_RemovesCustomer_WhenCustomerExists()
    {
        // Arrange
        var customer = customerRepository.Add("John", "Doe", new DateTime(1990, 1, 1));

        // Act
        customerRepository.Delete(customer.CustomerId);

        // Assert
        Assert.IsEmpty(customerRepository.GetAll());
    }

    [Test]
    public void Delete_ThrowsException_WhenCustomerDoesNotExist()
    {
        // Act & Assert
        Assert.Throws<BadHttpRequestException>(() => customerRepository.Delete(1));
    }
}
