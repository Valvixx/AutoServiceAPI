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

        public OrderController(OrderRepository orderRepository, OrderRepositorySQL orderRepositorySQL)
        {
            this.orderRepository = orderRepository;
            this.orderRepositorySQL = orderRepositorySQL;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Order>> Get()
        {
            List<Order> orders = orderRepositorySQL.GetAllOrders();

            return Ok(orders);
        }

        [HttpGet("{Id}")]
        public ActionResult<Order> Get([FromRoute]string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return BadRequest("Id can not be empty");

            Order order = orderRepositorySQL.GetOrderById(id);
            return Ok(order);
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
            OrderRepository orderRepository = new OrderRepository();
            var newOrder = new Order()
            {
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

            orderRepository.UpdateOrder( orderDTO.Id, orderDTO.CustomerInfo, orderDTO.CarInfo, orderDTO.Date, orderDTO.Description, orderDTO.Status);
            return Ok(newOrder);
;
        }

        [HttpDelete]
        public IActionResult Delete(string id)
        {
            OrderRepository orderRepository = new OrderRepository();
            bool result = orderRepository.DeleteOrder(id);
            if (string.IsNullOrWhiteSpace(id)) return BadRequest("Id can not be empty");
            if (!result) return NotFound($"Id:{id} not found");
            return Ok($"Order with Id:{id} was deleted");
        }

        [HttpGet("by-client/{phone}")]
        public ActionResult<List<Order>> GetOrdersByCustomer(string phone)
        {
            OrderRepository orderRepository = new OrderRepository();
            var orders = orderRepository.GetOrdersByCustomer(phone);
            if (string.IsNullOrWhiteSpace(phone)) return BadRequest("Phone can not be empty");
            return Ok(orders);
        }
    }
}
