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
        public ActionResult<Order> Get([FromRoute]string id)
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
        public ActionResult<Models.DbOrder> Post(DbOrder orderDTO)
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
        public ActionResult<Order> Put(DbOrder orderDTO)
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
