using System.Collections.Generic;
using AutoServiceAPI.Models;
namespace AutoService.Repository
{
    public class CarRepository
    {
        private List<Car> cars = new List<Car>
        {
            new Car { Brand = "Toyota", Model = "Camry", ReleaseYear = 2020, VIN = "VIN123456789" },
            new Car { Brand = "Honda", Model = "Civic", ReleaseYear = 2019, VIN = "VIN987654321" },
            new Car { Brand = "Ford", Model = "F-150", ReleaseYear = 2021, VIN = "VIN567890123" }
        };
        public List<Car> GetAllCars()
        {
            return cars;
        }
        public void AddCar(Car car) 
        {
            cars.Add(car);
        }
    }
}
