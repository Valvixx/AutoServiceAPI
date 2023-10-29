using Dapper;
using AutoService.Models;
using System.Data.Common;

namespace AutoService.Data.Repositories
{
    public class CustomerRepositorySQL
    {
        private readonly DbConnection dbConnection;

        public CustomerRepositorySQL(DbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        public List<Customer> GetAllCustomers()
        {
            return dbConnection.Query<Customer>("SELECT * from customers").ToList();
        }

        public Customer? GetCustomerByPhone(string phone)
        {
            var sql = @"SELECT * FROM customers WHERE phone = @Phone";

            return dbConnection.QueryFirstOrDefault<Customer>(sql, new { @Phone = phone });
        }

        public void AddCustomer(Customer customer)
        {
            var sql = @"INSERT INTO customers(name, ""adress"", phone)
                VALUES (@Name, @Adress, @Phone)";

            dbConnection.Execute(sql, customer);
        }

        public void UpdateCustomerByPhone(Customer customer)
        {
            var sql = @"UPDATE customers SET
                name = @Name,
                ""adress"" = @Adress
                WHERE phone = @Phone";

            dbConnection.Execute(sql, customer);
        }

        public bool DeleteCustomerByPhone(string phone)
        {
            var sql = @"DELETE FROM customers
                WHERE phone = @Phone";

            var rowsAffected = dbConnection.Execute(sql, new { Phone = phone });

            if (rowsAffected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
