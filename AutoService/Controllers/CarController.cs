using Microsoft.AspNetCore.Mvc;
using AutoService.Repository;
using AutoServiceAPI.Models;
using System.Collections.Generic;

namespace AutoService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private CarRepository carRepository;

        public CarController()
        {
            carRepository = new CarRepository();
        }

        [HttpGet]
        public IEnumerable<Car> Get()
        {
            List<Car> cars = carRepository.GetAllCars();
            return cars;
        }

        [HttpGet("{id}")]
        public Car Get(string id)
        {
            List<Car> cars = carRepository.GetAllCars();
            var SelectedCar = cars.FirstOrDefault(car => car.VIN == id);
            return SelectedCar;
        }

        [HttpPost]
        public Car Post(string brand, string model, int releaseYear, string vin)
        {
            Car newCar = new Car
            {
                Brand = brand,
                Model = model,
                ReleaseYear = releaseYear,
                VIN = vin
            };
            CarRepository carRepository = new CarRepository();
            carRepository.AddCar(newCar);
            return newCar;
        }
    }
}