using System.Reflection;
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

        public bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(Date) ||
                string.IsNullOrWhiteSpace(Description) ||
                string.IsNullOrWhiteSpace(Status) ||
                ! OrderCar.IsValid() ||
                ! User.IsValid())
            { return false; }
            return true;
        }
    }   
}
