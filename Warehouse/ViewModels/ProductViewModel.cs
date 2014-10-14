using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.ViewModels
{
    public class ProductViewModel
    {
        public string Name { get; set; }

        public int Quantity { get; set; }

        public int? SaleQuantity { get; set; }

        public decimal BuyPrice { get; set; }

        public decimal SellPrice { get; set; }

        public DateTime DayOfPurchase { get; set; }

        public DateTime DayOfSale { get; set; }

        public VendorViewModel Vendor { get; set; }
    }
}
