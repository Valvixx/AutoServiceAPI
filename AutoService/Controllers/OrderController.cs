using AutoService.Models;
using AutoService.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoService.Data.Repositories;

namespace AutoService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private OrderRepository orderRepository;
        private OrderRepositorySQL orderRepositorySQL;
        private CarRepositorySQL carRepositorySQL;
        private CustomerRepositorySQL customerRepositorySQL;

        public OrderController(OrderRepository orderRepository, OrderRepositorySQL orderRepositorySQL, CarRepositorySQL carRepositorySQL, CustomerRepositorySQL customerRepositorySQL)
        {
            this.orderRepository = orderRepository;
            this.orderRepositorySQL = orderRepositorySQL;
            this.carRepositorySQL = carRepositorySQL;
            this.customerRepositorySQL = customerRepositorySQL;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Order>> Get()
        {
            List<Models.DbOrder> orders = orderRepositorySQL.GetAllOrders();
            List<Order> result = new List<Order>();
            foreach (Models.DbOrder order in orders)
            {
                result.Add(new Order
                {
                    Id = order.Id,
                    Date = order.Date,
                    Description = order.Description,
                    Status = order.Status,
                    OrderCar = carRepositorySQL.GetCarByVin(order.Car),
                    User = customerRepositorySQL.GetCustomerByPhone(order.Customer)
                });
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult<Order> Get([FromRoute] string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return BadRequest("Id can not be empty");

            var order = orderRepositorySQL.GetOrderById(id);
            if (order == null) return NotFound("Order not found");
            var result = new Order()
            {
                Id = order.Id,
                Date = order.Date,
                Description = order.Description,
                Status = order.Status,
                OrderCar = carRepositorySQL.GetCarByVin(order.Car),
                User = customerRepositorySQL.GetCustomerByPhone(order.Customer)
            };
            return Ok(result);
        }

        [HttpPost]
        public ActionResult<Order> Post(OrderDTO orderDTO)
        {

            Order newOrder = new Order()
            {
                User = orderDTO.CustomerInfo,
                OrderCar = orderDTO.CarInfo,
                Date = orderDTO.Date,
                Description = orderDTO.Description,
                Status = orderDTO.Status,
                Id = orderDTO.Id
            };
            if (!newOrder.IsValid())
            {
                return BadRequest("Data is not valid");
            }

            orderRepositorySQL.AddOrder(newOrder);
            return Ok(newOrder);
        }

        [HttpPut]
        public ActionResult<Order> Put(OrderDTO orderDTO)
        {
            carRepositorySQL.UpdateCar(orderDTO.CarInfo);
            customerRepositorySQL.UpdateCustomerByPhone(orderDTO.CustomerInfo);
            var newOrder = new Order()
            {
                Id = orderDTO.Id,
                User = orderDTO.CustomerInfo,
                OrderCar = orderDTO.CarInfo,
                Date = orderDTO.Date,
                Description = orderDTO.Description,
                Status = orderDTO.Status
            };

            if (!newOrder.IsValid())
            {
                return BadRequest("Data is not valid");
            }

            orderRepositorySQL.UpdateOrderById(newOrder);
            return Ok(newOrder);
            ;
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute]string id)
        {
            
            if (string.IsNullOrWhiteSpace(id)) return BadRequest("Id can not be empty");;
            orderRepositorySQL.DeleteOrderById(id);
            return Ok($"Order with Id:{id} was deleted");
        }

        [HttpGet("by-client/{phone}")]
        public ActionResult<List<Order>> GetOrdersByCustomer(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return BadRequest("Phone can not be empty");

            var orders = orderRepositorySQL.GetOrdersByCustomer(phone);
            return Ok(orders);
        }
    }
}
