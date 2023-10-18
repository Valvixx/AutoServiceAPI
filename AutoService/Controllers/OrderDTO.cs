using AutoService.Models;
using AutoServiceAPI.Models;

namespace AutoService.Controllers
{
    public class CreateOrderDTO
    {
        public  Customer CustomerInfo { get; set; }
        public Car CarInfo { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Id { get; set; }
    }
}
