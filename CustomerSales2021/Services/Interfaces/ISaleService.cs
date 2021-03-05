using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerSales2021.Models;

namespace CustomerSales2021.Services.Interfaces
{
    public interface ISaleService
    {
        Task<List<Sale>> GetSalesByAmountAsync(int maxAmount);
        Task<List<Sale>> GetSalesAsync();
        Task AddSaleAsync(Sale sale);
        Task DeleteSaleAsync(Sale sale);

        Task<Sale> GetSaleByIdAsync(int id);
        Task<List<Sale>> GetSalesByCustomerIdAsync(int CustomerId);
    }
}
