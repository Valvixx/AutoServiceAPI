using AutoService.Models;
using AutoServiceAPI.Models;

namespace AutoService.Repository
{
    public class CustomerRepository
    {
        private List<Customer> customers = new List<Customer>
        {
            new Customer
            {
                Name = "Alexander Ivanov",
                Adress = "Moscow, Pushkin Street, 33",
                Phone = "89364857305",
                Cars = new List<Car>
                {
                    new Car { Brand = "Toyota", Model = "Camry", ReleaseYear = 2020, VIN = "VIN123456789" },
                    new Car { Brand = "Honda", Model = "Civic", ReleaseYear = 2019, VIN = "VIN987654321" }
                }

            },
            new Customer
            {
                Name = "Maria Petrova",
                Adress = "Saint Petersburg, Lenin Avenue, 25",
                Phone = "89051234567",
                Cars = new List<Car>
                {
                    new Car { Brand = "Nissan", Model = "Altima", ReleaseYear = 2018, VIN = "VIN1122334455" }
                }
            },
            new Customer
            {
                Name = "Ivan Kozlov",
                Adress = "Kazan, Gorky Street, 12",
                Phone = "89107654321",
                Cars = new List<Car>
                {
                    new Car { Brand = "Chevrolet", Model = "Malibu", ReleaseYear = 2019, VIN = "VIN5555666677" },
                    new Car { Brand = "Volkswagen", Model = "Jetta", ReleaseYear = 2022, VIN = "VIN9876543210" },
                    new Car { Brand = "Ford", Model = "Escape", ReleaseYear = 2021, VIN = "VIN6677889900" }
                }
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
                customerToUpdate.Adress = name;
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