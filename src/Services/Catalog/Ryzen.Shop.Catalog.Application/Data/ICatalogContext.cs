using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ryzen.Shop.Catalog.Domain;

namespace Ryzen.Shop.Catalog.Application.Data
{
    public  interface ICatalogContext
    {
         DbSet<Product> Products { get; set; }
         DbSet<Promotion> Promotions { get; set; }
        DbSet<PromotionType> PromotionTypes { get; set; }
    }
}
