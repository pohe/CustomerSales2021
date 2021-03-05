using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerSales2021.Models;
using CustomerSales2021.Services.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace CustomerSales2021.Services.SaleService
{
    public class ADO_SaleService: ISaleService
    {
        List<Sale> sales;
        string connectionString;
        public IConfiguration Configuration { get; }
        public ADO_SaleService(IConfiguration configuration)
        {
            Configuration = configuration;
            connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            sales = new List<Sale>();
        }

        public async Task<List<Sale>> GetSalesAsync()
        {
            string sql = "Select * From Sale";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                using (SqlDataReader dataReader = await command.ExecuteReaderAsync())
                {
                    while (dataReader.Read())
                    {
                        Sale @sale = new Sale();
                        @sale.SaleId = Convert.ToInt32(dataReader["SaleId"]);
                        @sale.Amount = Convert.ToInt32(dataReader["Amount"]);
                        @sale.SalesDate = Convert.ToDateTime(dataReader["SalesDate"]);
                        @sale.CustomerId = Convert.ToInt32(dataReader["CustomerId"]);
                        sales.Add(@sale);
                    }
                }
            }
            return sales;
        }
        public async Task<List<Sale>> GetSalesByCustomerIdAsync(int id)
        {
            string sql = "Select * From Sale Where CustomerId=@id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@id", id);
                using (SqlDataReader dataReader = await command.ExecuteReaderAsync())
                {
                    while (dataReader.Read())
                    {
                        Sale @event = new Sale();
                        @event.SaleId = Convert.ToInt32(dataReader["SaleId"]);
                        @event.ProductName = Convert.ToString(dataReader["ProductName"]);
                        @event.Amount = Convert.ToInt32(dataReader["Amount"]);
                        @event.SalesDate = Convert.ToDateTime(dataReader["SalesDate"]);
                        @event.CustomerId = Convert.ToInt32(dataReader["CustomerId"]);
                        sales.Add(@event);
                    }
                }
            }
            return sales;
        }

        

        public async Task AddSaleAsync(Sale sale)
        {
            string sql = $"Insert Into Sale (Amount, SalesDate , CustomerId, ProductName) Values (@Amount,@SalesDate, @CustomerId , @ProductName)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Amount", sale.Amount);
                    command.Parameters.AddWithValue("@CustomerId", sale.CustomerId);
                    command.Parameters.AddWithValue("@SalesDate", sale.SalesDate);
                    command.Parameters.AddWithValue("@ProductName", sale.ProductName);
                    int affectedRows = await command.ExecuteNonQueryAsync();
                }
            }
        }
        public async Task<Sale> GetSaleByIdAsync(int id)
        {
            Sale @event = new Sale();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string sql = $"Select * From Sale Where SaleId=@id ";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    SqlDataReader dataReader = await command.ExecuteReaderAsync();
                    while (dataReader.Read())
                    {
                        @event.SaleId = Convert.ToInt32(dataReader["SaleId"]);
                        @event.ProductName = Convert.ToString(dataReader["ProductName"]);
                        @event.SalesDate = Convert.ToDateTime(dataReader["SalesDate"]);
                        @event.Amount = Convert.ToInt32(dataReader["Amount"]);
                    }
                }
            }
            return @event;
        }
        public async Task DeleteSaleAsync(Sale sale)
        {
            string sql = $"Delete From Sale Where SaleId=@id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", sale.SaleId);
                    int affectedRows = await command.ExecuteNonQueryAsync();
                }
            }
        }
        public async Task<List<Sale>> GetSalesByAmountAsync(int maxAmount)
        {
            string sql = "Select * From Sale where Amount < @amount ";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@amount", maxAmount);
                using (SqlDataReader dataReader = await command.ExecuteReaderAsync())
                {
                    while (dataReader.Read())
                    {
                        Sale @sale = new Sale();
                        @sale.SaleId = Convert.ToInt32(dataReader["SaleId"]);
                        @sale.Amount = Convert.ToInt32(dataReader["Amount"]);
                        @sale.CustomerId = Convert.ToInt32(dataReader["CustomerId"]);
                        @sale.ProductName = Convert.ToString(dataReader["ProductName"]);
                        @sale.SalesDate = Convert.ToDateTime(dataReader["SalesDate"]);
                        sales.Add(@sale);
                    }
                }
            }
            return sales;
        }
    }
}
