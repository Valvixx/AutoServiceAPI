using AutoService.Models;
using AutoService.Repository;
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
        public ActionResult<IEnumerable<Order>> Get()
        {
            List<Order> orders = orderRepository.GetAllOrders();
            return Ok(orders);
        }

        [HttpGet("{Id}")]
        public ActionResult<Order> Get(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return BadRequest("Id can not be empty");
            List<Order> orders = orderRepository.GetAllOrders();
            Order SelectedOrder = orders.FirstOrDefault(orders => orders.Id == id);
            return Ok(SelectedOrder);
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
            OrderRepository orderRepository = new OrderRepository();
            orderRepository.AddOrder(newOrder);
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
