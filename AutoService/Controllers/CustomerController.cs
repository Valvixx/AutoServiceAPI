using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoServiceAPI.Models;
using AutoService.Repository;
using AutoService.Models;

namespace AutoService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private CustomerRepository customerRepository;
        public CustomerController(CustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        [HttpGet]
        public IEnumerable<Customer> Get()
        {
            List<Customer> customers = customerRepository.GetAllCustomers();
            return customers;
        }

        [HttpGet("{Name}")]
        public Customer Get(string name)
        {
            List<Customer> customers = customerRepository.GetAllCustomers();
            Customer SelectedCustomer = customers.FirstOrDefault(customers => customers.Name == name);
            return SelectedCustomer;
        }

        [HttpPost]
        public Customer Post(string name, string adress, string phone)
        {
            Customer newCustomer = new Customer()
            {
                Name = name,
                Adress = adress,
                Phone = phone
            };
            CustomerRepository customerRepository = new CustomerRepository();
            customerRepository.AddCustomer(newCustomer);
            return newCustomer;
        }

        [HttpPut]
        public Customer Put(string name, string adress, string phone)
        {
            CustomerRepository customerRepository = new CustomerRepository();
            customerRepository.UpdateCustomer(name, adress, phone);
            return new Customer
            {
                Name = name,
                Adress = adress,
                Phone = phone
            };
        }

        [HttpDelete]
        public void Delete(string name)
        {
            CustomerRepository customerRepository = new CustomerRepository();
            customerRepository.DeleteCustomer(name);
        }
    }
}