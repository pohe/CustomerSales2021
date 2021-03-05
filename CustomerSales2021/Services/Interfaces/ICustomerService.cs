using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerSales2021.Models;

namespace CustomerSales2021.Services.Interfaces
{

    public interface ICustomerService
    {
        Task<List<Customer>> GetCustomersByNameAsync(string name);
        Task<List<Customer>> GetCustomersAsync();
        Task AddCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(Customer customer);
        Task<Customer> GetCustomerByIdAsync(int id);

    }
    
}
