using AutoService.Models;
using AutoService.Repository;
using AutoServiceAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutoService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private OrderRepository orderRepository;
        public OrderController(OrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        [HttpGet]
        public IEnumerable<Order> Get()
        {
            List<Order> orders = orderRepository.GetAllOrders();
            return orders;
        }

        [HttpGet("{Id}")]
        public Order Get(string id)
        {
            List<Order> orders = orderRepository.GetAllOrders();
            Order SelectedOrder = orders.FirstOrDefault(orders => orders.Id == id);
            return SelectedOrder;
        }

        [HttpPost]
        public Order Post(string id, Customer user,Car orderCar, string date, string description, string status)
        {
            Order newOrder = new Order()
            {
                User = user,
                OrderCar = orderCar,
                Date = date,
                Description = description,
                Status = status
        };
            OrderRepository orderRepository = new OrderRepository();
            orderRepository.AddOrder(newOrder);
            return newOrder;
        }

        [HttpPut]
        public Order Put(string id, Customer user, Car orderCar, string date, string description, string status)
        {
            OrderRepository orderRepository = new OrderRepository();
            orderRepository.UpdateOrder(id, user, orderCar, date, description, status);
            return new Order()
            {
                User = user,
                OrderCar = orderCar,
                Date = date,
                Description = description,
                Status = status
            };
        }

        [HttpDelete]
        public void Delete(string id)
        {
            OrderRepository orderRepository = new OrderRepository();
            orderRepository.DeleteOrder(id);
        }
    }
}
