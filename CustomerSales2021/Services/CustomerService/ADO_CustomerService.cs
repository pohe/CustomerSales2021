using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerSales2021.Models;
using CustomerSales2021.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;

namespace CustomerSales2021.Services.CustomerService
{
    public class ADO_CustomerService: ICustomerService
    {
        List<Customer> customers;
        string connectionString;
        public IConfiguration Configuration { get; }
        public ADO_CustomerService(IConfiguration configuration)
        {
            Configuration = configuration;
            connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            customers = new List<Customer>();
        }

        public async Task<List<Customer>> GetCustomersAsync()
        {
            string sql = "Select * From Customer ";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand(sql, connection);
                using (SqlDataReader dataReader = await command.ExecuteReaderAsync())
                {
                    while (await dataReader.ReadAsync())
                    {
                        Customer @cust = new Customer();
                        @cust.CustomerId = Convert.ToInt32(dataReader["CustomerId"]);
                        @cust.Name = Convert.ToString(dataReader["Name"]);
                        @cust.Address = Convert.ToString(dataReader["Address"]);
                        @cust.Age = Convert.ToInt32(dataReader["Age"]);
                        customers.Add(@cust);
                    }
                }
            }
            return customers;
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            string sql = $"Insert Into Customer (Name, Address, Age) Values (@Name,@Address,@Age)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Name", customer.Name);
                    command.Parameters.AddWithValue("@Address", customer.Address);
                    command.Parameters.AddWithValue("@Age", customer.Age);
                    int affectedRows = await command.ExecuteNonQueryAsync();
                }
            }
        }

        public void UpdateCustomer(Customer @customer)
        {
            string sql = $"Update Customer SET (Name, Address, Age) Values (@Name,@Address,@Age)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", @customer.CustomerId);
                    command.Parameters.AddWithValue("@Name", @customer.Name);
                    command.Parameters.AddWithValue("@Address", @customer.Address);
                    command.Parameters.AddWithValue("@Age", @customer.Age);
                    int affectedRows = command.ExecuteNonQuery();
                }
            }
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            Customer @customer = new Customer();
            string sql = $"Select * From Customer  Where CustomerId=@id ";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    SqlDataReader dataReader = await command.ExecuteReaderAsync();

                    while (dataReader.Read())
                    {
                        @customer.CustomerId = Convert.ToInt32(dataReader["CustomerId"]);
                        @customer.Name = Convert.ToString(dataReader["Name"]);
                        @customer.Address = Convert.ToString(dataReader["Address"]);
                        @customer.Age = Convert.ToInt32(dataReader["Age"]);

                    }
                }
            }
            return @customer;
        }

        public async Task DeleteCustomerAsync(Customer customer)
        {
            string sql = $"Delete From Customer  Where CustomerId=@id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", customer.CustomerId);
                    int affectedRows = await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<List<Customer>> GetCustomersByNameAsync(string name)
        {
            string sql = "Select * From Customer  where Name LIKE'" + @name + "%" + "'";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@name", name);
                using (SqlDataReader dataReader = await command.ExecuteReaderAsync())
                {
                    while (await dataReader.ReadAsync())
                    {
                        Customer @customer = new Customer();
                        @customer.CustomerId = Convert.ToInt32(dataReader["CustomerId"]);
                        @customer.Name = Convert.ToString(dataReader["Name"]);
                        @customer.Address = Convert.ToString(dataReader["Address"]);
                        @customer.Age = Convert.ToInt32(dataReader["Age"]);
                        customers.Add(@customer);
                    }
                }
            }
            return customers;
        }
    }
}
