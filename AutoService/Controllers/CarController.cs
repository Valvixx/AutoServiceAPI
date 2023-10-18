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

        public CarController(CarRepository carRepository)
        {
            this.carRepository = carRepository;
        }

        [HttpGet]
        public IEnumerable<Car> Get()
        {
            List<Car> cars = carRepository.GetAllCars();
            return cars;
        }

        [HttpGet("{VIN}")]
        public Car Get(string vin)
        {
            List<Car> cars = carRepository.GetAllCars();
            Car SelectedCar = cars.FirstOrDefault(car => car.VIN == vin);
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
        [HttpPut]
        public Car Put(string vin, string brand, string model, int releaseYear)
        {
            CarRepository carRepository = new CarRepository();
            carRepository.UpdateCar(vin, brand, model, releaseYear);
            return new Car
            {
                Brand = brand,
                Model = model,
                ReleaseYear = releaseYear,
                VIN = vin
            };
        }
    }
}