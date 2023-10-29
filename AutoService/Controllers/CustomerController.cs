using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoService.Repository;
using AutoService.Models;
using AutoService.Data.Repositories;

namespace AutoService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private CustomerRepository customerRepository;
        private CustomerRepositorySQL customerRepositorySQL;
        public CustomerController(CustomerRepository customerRepository, CustomerRepositorySQL customerRepositorySQL)
        {
            this.customerRepository = customerRepository;
            this.customerRepositorySQL = customerRepositorySQL;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Customer>> Get()
        {
            List<Customer> customers = customerRepositorySQL.GetAllCustomers();

            return Ok(customers);
        }

        [HttpGet("{phone}")]
        public ActionResult<Customer> Get([FromRoute]string phone, CustomerRepositorySQL customerRepositorySQL)
        {
            Customer customer = customerRepositorySQL.GetCustomerByPhone(phone);

            return Ok(customer);
        }

        [HttpPost]
        public ActionResult<Customer> Post(string name, string adress, string phone)
        {
            Customer newCustomer = new Customer()
            {
                Name = name,
                Adress = adress,
                Phone = phone
            };

            if (!newCustomer.IsValid())
            {
                return BadRequest("Data is not valid");
            }

            customerRepositorySQL.AddCustomer(newCustomer);

            return Ok(newCustomer);
        }

        [HttpPut]
        public ActionResult<Customer> Put(string name, string adress, string phone)
        {
            var newCustomer = new Customer
            {
                Name = name,
                Adress = adress,
                Phone = phone
            };

            if (!newCustomer.IsValid())
            {
                return BadRequest("Data is not valid");
            }

            customerRepositorySQL.UpdateCustomerByPhone(newCustomer);

            return Ok(newCustomer);
        }

        [HttpDelete]
        public IActionResult Delete(string phone)
        {
            bool result = customerRepositorySQL.DeleteCustomerByPhone(phone);
            if (string.IsNullOrWhiteSpace(phone)) return BadRequest("Phone can not be empty");
            if (!result) return NotFound($"Phone:{phone} not found");
            return Ok($"Customer with phone:{phone} was deleted");
        }
    }
}