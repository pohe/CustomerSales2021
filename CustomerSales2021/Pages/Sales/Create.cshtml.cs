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
    public class CreateModel : PageModel
    {
        [BindProperty]
        public Sale Sale { get; set; } = new Sale();

        ISaleService saleService;
        ICustomerService customerService;

        public CreateModel(ISaleService sService, ICustomerService cService)
        {
            this.saleService = sService;
            customerService = cService;
        }
        public IActionResult OnGet(int id)
        {
            Sale.CustomerId = id;
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Sale.SalesDate = DateTime.Now;
            await saleService.AddSaleAsync(Sale);
            return RedirectToPage("GetAllSales");
        }
    }
}
