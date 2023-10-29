using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public ActionResult<IEnumerable<Customer>> Get()
        {
            return Ok(customers);
        }

        [HttpGet("{Name}")]
        public ActionResult<Customer> Get(string name)
        {
            List<Customer> customers = customerRepository.GetAllCustomers();
            Customer SelectedCustomer = customers.FirstOrDefault(customers => customers.Name == name);
            return Ok(SelectedCustomer);
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
            CustomerRepository customerRepository = new CustomerRepository();
            customerRepository.AddCustomer(newCustomer);
            return Ok(newCustomer);
        }

        [HttpPut]
        public ActionResult<Customer> Put(string name, string adress, string phone)
        {
            CustomerRepository customerRepository = new CustomerRepository();
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
            customerRepository.UpdateCustomer(name, adress, phone);
            return Ok(newCustomer);
        }

        [HttpDelete]
        public IActionResult Delete(string phone)
        {
            CustomerRepository customerRepository = new CustomerRepository();
            
            bool result = customerRepository.DeleteCustomer(phone);
            if (string.IsNullOrWhiteSpace(phone)) return BadRequest("Phone can not be empty");
            if (!result) return NotFound($"Phone:{phone} not found");
            return Ok($"Customer with phone:{phone} was deleted");
        }
    }
}