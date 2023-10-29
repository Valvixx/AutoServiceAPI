using Dapper;
using AutoService.Models;
using System.Data.Common;

namespace AutoService.Data.Repositories
{
    public class OrderRepositorySQL
    {
        private readonly DbConnection dbConnection;

        public OrderRepositorySQL(DbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        public List<DbOrder> GetAllOrders()
        {
            return dbConnection.Query<DbOrder>("SELECT * FROM orders JOIN cars ON cars.vin = orders.car").ToList();
        }

        public DbOrder? GetOrderById(string id)
        {
            var sql = @"SELECT * FROM orders WHERE id = @Id";

            return dbConnection.QueryFirstOrDefault<DbOrder>(sql, new { @Id = id });
        }

        public void AddOrder(Order order)
        {
            var sql = @"INSERT INTO orders(id, car, customer, date, description, status)
                VALUES (@Id, @Vin, @User, @Date, @Description, @Status)";

            dbConnection.Execute(sql, new {@Id = order.Id, @Vin = order.OrderCar.VIN, @User = order.User.Phone, @Date = order.Date, @Description = order.Description, @Status = order.Status });
        }

        public void UpdateOrderById(Order order)
        {
            var sql = @"UPDATE orders SET
                car = @Vin,
                customer = @User,
                date = @Date,
                description = @Description,
                status = @Status
                WHERE id = @Id";

            dbConnection.Execute(sql, new {@Vin = order.OrderCar.VIN, @User = order.User.Phone, @Date = order.Date, @Description = order.Description, @Ststus = order.Status});
        }
    }
}
