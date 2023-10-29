using Microsoft.AspNetCore.Mvc;
using AutoService.Repository;
using System.Collections.Generic;
using AutoService.Models;
using AutoService.Data.Repositories;
using System.Numerics;

namespace AutoService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private CarRepository carRepository;
        private CarRepositorySQL carRepositorySQL;


        public CarController(CarRepository carRepository, CarRepositorySQL carRepositorySQL)
        {
            this.carRepository = carRepository;
            this.carRepositorySQL = carRepositorySQL;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Car>> Get()
        {
            List<Car> cars = carRepositorySQL.GetAllCars();

            return Ok(cars);
        }

        [HttpGet("{vin}")]
        public ActionResult<Car> Get([FromRoute] string vin)
        {
            Car car = carRepositorySQL.GetCarByVin(vin);
            if (string.IsNullOrWhiteSpace(vin)) return BadRequest("VIN can not be empty");

            return Ok(car);
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

            carRepositorySQL.AddCar(newCar);

            return Ok(newCar);
        }

        [HttpPut]
        public ActionResult<Car> Put(string vin, string brand, string model, int releaseYear)
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

            carRepositorySQL.UpdateCar(newCar);

            return Ok(newCar);
        }

        [HttpDelete]
        public ActionResult DeleteCar(string vin) 
        {
            carRepositorySQL.DeleteCar(vin);
            
            return Ok("Deleted");
        }

        [HttpGet("by-client/{phone}")]
        public ActionResult<List<Car>> GetCarsByCustomer(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return BadRequest("Phone can not be empty");

            var cars = carRepositorySQL.GetCustomerCars(phone);
            return Ok(cars);
        }
    }
}