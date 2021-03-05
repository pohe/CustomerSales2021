using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerSales2021.Models;
using CustomerSales2021.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CustomerSales2021.Pages.Sales
{
    public class GetAllSalesModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int MaxAmount { get; set; }
        public IEnumerable<Sale> Sales { get; private set; }

        [BindProperty]
        public Customer Customer { get; set; }

        ISaleService saleService;
        public GetAllSalesModel(ISaleService service)
        {
            this.saleService = service;
        }
        public async Task OnGetAsync()
        {
            if (MaxAmount > 0)
            {
                Sales = await saleService.GetSalesByAmountAsync(MaxAmount);
            }
            else
                Sales = await saleService.GetSalesAsync();
        }
        public async Task OnGetMySalesAsync(int cid)
        {
            Sales = await saleService.GetSalesByCustomerIdAsync(cid);
        }
    }
}
