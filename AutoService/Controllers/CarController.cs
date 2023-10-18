using Microsoft.AspNetCore.Mvc;
using AutoService.Repository;
using AutoServiceAPI.Models;
using System.Collections.Generic;
using AutoService.Models;
using System.Numerics;

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
        public ActionResult<IEnumerable<Car>> Get()
        {
            List<Car> cars = carRepository.GetAllCars();

            return Ok(cars);
        }

        [HttpGet("{VIN}")]
        public ActionResult<Car> Get(string vin)
        {
            List<Car> cars = carRepository.GetAllCars();
            Car SelectedCar = cars.FirstOrDefault(car => car.VIN == vin);
            if (string.IsNullOrWhiteSpace(vin)) return BadRequest("VIN can not be empty");
            return Ok(SelectedCar);
        }

        [HttpPost]
        public ActionResult<Car> Post(string brand, string model, int releaseYear, string vin)
        {
            Car newCar = new Car
            {
                Brand = brand,
                Model = model,
                ReleaseYear = releaseYear,
                VIN = vin
            };
            if (!newCar.IsValid())
            {
                return BadRequest("Data is not valid");
            }
            CarRepository carRepository = new CarRepository();
            carRepository.AddCar(newCar);
            return Ok(newCar);
        }

        [HttpPut]
        public ActionResult<Car> Put(string vin, string brand, string model, int releaseYear)
        {
            CarRepository carRepository = new CarRepository();
            Car newCar = new Car
            {
                Brand = brand,
                Model = model,
                ReleaseYear = releaseYear,
                VIN = vin
            };
            if (!newCar.IsValid())
            {
                return BadRequest("Data is not valid");
            }
            carRepository.UpdateCar(vin, brand, model, releaseYear);
            return Ok(newCar);
        }

        [HttpGet("by-client/{phone}")]
        public ActionResult<List<Car>> GetCarsByCustomer(string phone)
        {
            CarRepository carRepository = new CarRepository();
            var cars = carRepository.GetAllCustomerCars(phone);
            if (string.IsNullOrWhiteSpace(phone)) return BadRequest("Phone can not be empty");
            return Ok(cars);
        }
    }
}