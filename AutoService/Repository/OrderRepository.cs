using AutoService.Models;
using AutoServiceAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace AutoService.Repository
{
    public class OrderRepository
    {
        public static List<Order> orders = new List<Order>()
        {
            new Order
            {
                OrderCar = CarRepository.cars[0],
                User = CustomerRepository.customers[1],
                Date = "20.09.2023",
                Description = "Replacement of brake pads",
                Status = "Completed",
                Id = "1237654"
            },
            new Order
            {
                OrderCar = CarRepository.cars[1],
                User = CustomerRepository.customers[1],
                Date = "16.10.2023",
                Description = "Repair after coolant leak",
                Status = "In process",
                Id = "3213765"
            },
            new Order
            {
                OrderCar = CarRepository.cars[2],
                User = CustomerRepository.customers[2],
                Date = "14.10.2023",
                Description = "Recovery from scratches and dents",
                Status = "In process",
                Id = "3219547"
            }
        };
        public List<Order> GetAllOrders()
        {
            return orders;
        }
        public void AddOrder(Order order)
        {
            orders.Add(order);
        }
        public void UpdateOrder(string id, Customer user, Car orderCar, string date, string description, string status)
        {
            Order orderToUpdate = orders.FirstOrDefault(orders => orders.Id == id);
            if (orderToUpdate != null)
            {
                orderToUpdate.User = user;
                orderToUpdate.OrderCar = orderCar;
                orderToUpdate.Date = date;
                orderToUpdate.Description = description;
                orderToUpdate.Status = status;
            }
        }
        public bool DeleteOrder(string id)
        {
            Order orderToDelete = orders.FirstOrDefault(orders => orders.Id == id);
            if (orderToDelete != null)
            {
                orders.Remove(orderToDelete);
                return true;
            }
            return false;
        }

        public List<Order> GetOrdersByCustomer(string phone)
        {
            var customerOrders = orders.Where(o => o.User.Phone == phone).ToList();
            return customerOrders;
        }


    }
}
