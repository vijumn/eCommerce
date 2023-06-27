using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ryzen.Shop.Shared;

namespace Ryzen.Shop.Catalog.Domain
{
    public class Product:Entity
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int? PromotionID { get; set; }
        public string Picture { get; set; }

        // Navigation property
        public Promotion? ProductPromotion { get; set; }
    }
}
