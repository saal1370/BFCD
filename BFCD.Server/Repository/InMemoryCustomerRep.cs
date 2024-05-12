

using BFCD.Server.Domain;

public class InMemoryCustomerRep : ICustomerRepository
{
    private readonly List<Customer> customers = new(); 

    public IEnumerable<Customer> GetAll() => customers;

    public Customer GetById(int id)
    {
        var customer = customers.FirstOrDefault(c => c.CustomerId == id);

        if (customer == null)
        {
            // If customer is null, throw BadRequestException
            throw new BadHttpRequestException($"Customer with ID {id} not found.");
        }

        return customer;
    }
#pragma warning restore CS8603 // Possible null reference return.
    public Customer Add(String name, String lastName, DateTime birthday)
    {
        var customer = new Customer(name, lastName, birthday);  
        if (customers.Any())
            customer.CustomerId = customers.Max(c => c.CustomerId) + 1;
        else
            customer.CustomerId = 1;

        customers.Add(customer);
        return customer;
    }

    public void Update(Customer customer)
    {
        var existingCustomer = customers.FirstOrDefault(c => c.CustomerId.Equals(customer.CustomerId));
        if (existingCustomer != null)
        {
            existingCustomer.Name = customer.Name;
            existingCustomer.LastName = customer.LastName;
            existingCustomer.Birthdag = customer.Birthdag;
        }
    }

    public void Delete(int customerId)
    {
        var customer = customers.FirstOrDefault(c => c.CustomerId == customerId);
        if (customer != null)
        {
            customers.Remove(customer);
        }
    }
}