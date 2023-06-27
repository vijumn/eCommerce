using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ryzen.Shop.Shared;

namespace Ryzen.Shop.Catalog.Domain
{
    public class Promotion
    {
        public int PromotionID { get; set; }
        public string Name { get; set; }
        public PromotionType Type { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public decimal? MinimumSpendDiscountAmount { get; set; }
        public decimal? MinimumSpendAmount { get; set; }

        public bool? GetOneFree { get; set; }
        public decimal? SecondOneDiscountPercentage { get; set; }

        // Navigation properties
        public ICollection<Product> Products { get; set; }
    }
   
}
