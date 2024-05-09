

using BFCD.Server.Domain;

public class InMemoryCustomerRep : ICustomerRepository
{
    private readonly List<Customer> customers = new(); 

    public IEnumerable<Customer> GetAll() => customers;
#pragma warning disable CS8603 // Possible null reference return.
    public Customer GetById(int id) => customers.FirstOrDefault(c => c.CustomerId == id);
#pragma warning restore CS8603 // Possible null reference return.
    public void Add(Customer customer)
    {
        if (customers.Any())
            customer.CustomerId = customers.Max(c => c.CustomerId) + 1;
        else
            customer.CustomerId = 1;

        customers.Add(customer);
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