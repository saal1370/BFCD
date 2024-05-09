using BFCD.Server.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BFCD.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRep;

        // Constructor accepting the DbContext as a dependency
        public CustomerController(ICustomerRepository customerRep)
        {
            _customerRep = customerRep;
        }

        [HttpPost("AddCustomers")]
        public IActionResult AddCustomers([FromBody] List<Customer> customers)
        {
            if (customers.IsNullOrEmpty())
            {
                return BadRequest("Customer cannot be null.");
            }

            foreach (var customer in customers)
            {
                _customerRep.Add(customer);
            }

            // Return the created customer with a 201 status and the URL of the created resource
            return Created(nameof(AddCustomers), customers);
        }

        [HttpGet("GetAllCustomers")]
        public IActionResult GetAllCustomers()
        {
            // Query the database for all customers
            var customers = _customerRep.GetAll();

            // Return the list of customers
            return Ok(customers);
        }

        [HttpGet("GetCustomerById")]
        public IActionResult GetCustomerById(int id)
        {
            // Query the database for unique customer
            var customer = _customerRep.GetById(id);

            // Return the unique customer
            return Ok(customer);
        }

        [HttpPost("UpdateCustomer")]
        public IActionResult UpdateCustomer(Customer customer)
        {
            // Query the database to update a customer
            _customerRep.Update(customer);

            // Return the unique updated customer
            return Ok(customer);
        }

        [HttpPost("DeleteCustomers")]
        public IActionResult DeleteCustomers([FromBody] List<int> customers)
        {
            // Query the database to delete one or more customers
            foreach (var customer in customers)
            {
                _customerRep.Delete(customer);
            }

            // Return the unique customer
            return Ok();
        }

    }
}