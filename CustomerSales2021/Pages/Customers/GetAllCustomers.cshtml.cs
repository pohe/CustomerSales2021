using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerSales2021.Models;
using CustomerSales2021.Services.CustomerService;
using CustomerSales2021.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CustomerSales2021.Pages.Customers
{
    public class GetAllCustomersModel : PageModel
    {
        
        [BindProperty(SupportsGet = true)]
        public string FilterCriteria { get; set; }
        public IEnumerable<Customer> Customers { get; private set; }

        private ICustomerService customerService;
       
        public GetAllCustomersModel(ICustomerService cService)
        {
            this.customerService = cService;
        }
        public async Task OnGetAsync()
        {
            if (!String.IsNullOrEmpty(FilterCriteria))
            {
                Customers = await customerService.GetCustomersByNameAsync(FilterCriteria);
            }
            else
                Customers = await customerService.GetCustomersAsync();
        }

    }
}
