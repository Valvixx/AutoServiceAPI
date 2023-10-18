using AutoServiceAPI.Models;
namespace AutoService.Models
{
    public class Order
    {
        public Car OrderCar { get; set; }
        public Customer User { get; set; }
        public string Date { get; set;}
        public string Description { get; set;}
        public string Status { get; set;}
        public string Id { get; set;}
    }
}
