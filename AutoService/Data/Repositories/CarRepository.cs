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

        public Car GetCarByVin(string carVin) 
        {
            var sql = @"SELECT * FROM cars WHERE vin in @Vin";
            return dbConnection.QueryFirstOrDefault<Car>(sql, new { Vin = carVin });
        }
    }
}
