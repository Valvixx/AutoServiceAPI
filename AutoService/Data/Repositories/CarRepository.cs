using Dapper;
using AutoService.Models;
using System.Data.Common;

namespace AutoService.Data.Repositories
{
    public class CarRepositorySQL
    {
        private readonly DbConnection dbConnection;

        public CarRepositorySQL(DbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        public List<Car> GetAllCars()
        {
            return dbConnection.Query<Car>("SELECT * from cars").ToList();
        }

        public Car? GetCarByVin(string carVin)
        {
            var sql = @"SELECT * FROM cars WHERE vin = @Vin";

            return dbConnection.QueryFirstOrDefault<Car>(sql, new { @Vin = carVin });
        }

        public void AddCar(Car car)
        {
            var sql = @"INSERT INTO cars(brand, model, ""releaseYear"", vin)
                VALUES (@Brand, @Model, @ReleaseYear, @VIN)";

            dbConnection.Execute(sql, car);
        }

        public void UpdateCar(Car car)
        {
            var sql = @"UPDATE cars SET
                brand = @Brand,
                ""model"" = @Model,
                ""releaseYear"" = @ReleaseYear
                WHERE vin = @VIN";

            dbConnection.Execute(sql, car);
        }

        public void DeleteCar(string vin)
        {
            var sql = @"DELETE FROM cars
                        WHERE vin = @VIN";

            dbConnection.Execute(sql, new { VIN = vin });
        }
    }
}
