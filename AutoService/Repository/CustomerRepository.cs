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
        public void UpdateCustomer(string name, string adress, string phone)
        {
            Customer customerToUpdate = customers.FirstOrDefault(customers => customers.Name == name);
            if (customerToUpdate != null)
            {
                customerToUpdate.Adress = adress;
                customerToUpdate.Phone = phone;
            }
        }
        public void DeleteCustomer(string name)
        {
            Customer customerToDelete = customers.FirstOrDefault(customers => customers.Name == name);
            if (customerToDelete != null)
            {
                customers.Remove(customerToDelete);
            }
        }
    }
}