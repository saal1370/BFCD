using BFCD.Server.Domain;
using Microsoft.AspNetCore.Routing;
using NuGet.ContentModel;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

[TestFixture]
public class InMemoryCustomerRepTest
{
    private InMemoryCustomerRep _customerRepository;

    [SetUp]
    public void Setup()
    {
        _customerRepository = new InMemoryCustomerRep();
    }

    [Test]
    public void GetAll_Returns_All_Customers()
    {
        // Arrange
        var expectedCustomersCount = 0;

        // Act
        var result = _customerRepository.GetAll();

        // Assert
        Assert.Equals(expectedCustomersCount, result.Count());
    }

    [Test]
    public void GetById_Returns_Customer_With_Valid_Id()
    {
        // Arrange
        var customerId = 1;
        var name = "John";
        var lastName = "Doe";
        var birthday = new DateTime(1990, 1, 1);
        var expectedCustomer = new Customer(name, lastName, birthday);
        expectedCustomer.CustomerId = customerId;
        _customerRepository.Add(name, lastName, birthday);

        // Act
        var result = _customerRepository.GetById(customerId);

        // Assert
        Assert.That(result != null);
        Assert.Equals(expectedCustomer.CustomerId, result.CustomerId);
        Assert.Equals(expectedCustomer.Name, result.Name);
        Assert.Equals(expectedCustomer.LastName, result.LastName);
        Assert.Equals(expectedCustomer.Birthdag, result.Birthdag);
    }

    [Test]
    public void GetById_Throws_Exception_With_Invalid_Id()
    {
        // Arrange
        var invalidCustomerId = 100;

        // Act & Assert
        Assert.Throws<BadHttpRequestException>(() => _customerRepository.GetById(invalidCustomerId));
    }

    // Add more tests for Add, Update, and Delete methods if needed
}
