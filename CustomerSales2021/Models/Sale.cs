using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerSales2021.Models
{
    public class Sale
    {
        public int SaleId { get; set; }
        public int CustomerId { get; set; }
        public string ProductName { get; set; }
        public DateTime SalesDate { get; set; }
        public int Amount { get; set; }
    }
}
