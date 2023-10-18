using AutoService.Models;
using AutoServiceAPI.Models;

namespace AutoService.Repository
{
    public class CustomerRepository
    {
        public static List<Customer> customers = new List<Customer>
        {
            new Customer
            {
                Name = "Alexander Ivanov",
                Adress = "Moscow, Pushkin Street, 33",
                Phone = "89364857305"
            },
            new Customer
            {
                Name = "Maria Petrova",
                Adress = "Saint Petersburg, Lenin Avenue, 25",
                Phone = "89051234567"
            },
            new Customer
            {
                Name = "Ivan Kozlov",
                Adress = "Kazan, Gorky Street, 12",
                Phone = "89107654321"
            }
        };

        public List<Customer> GetAllCustomers()
        {
            return customers;
        }
        public void AddCustomer(Customer customer)
        {
            customers.Add(customer);
        }
        public void UpdateCustomer(string phone, string name, string adress)
        {
            Customer customerToUpdate = customers.FirstOrDefault(customers => customers.Phone == phone);
            if (customerToUpdate != null)
            {
                customerToUpdate.Adress = adress;
                customerToUpdate.Name = name;
            }
        }
        public bool DeleteCustomer(string phone)
        {
            Customer customerToDelete = customers.FirstOrDefault(customers => customers.Phone == phone);
            if (customerToDelete != null)
            {
                customers.Remove(customerToDelete);
                return true;
            }
            return false;
        }
    }
}