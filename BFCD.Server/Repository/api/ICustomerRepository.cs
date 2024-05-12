
using BFCD.Server.Domain;

public interface ICustomerRepository
{
    IEnumerable<Customer> GetAll();
    Customer GetById(int id);
    Customer Add(String name, String lastName, DateTime birthday);
    void Update(Customer customer);
    void Delete(int customer);
}