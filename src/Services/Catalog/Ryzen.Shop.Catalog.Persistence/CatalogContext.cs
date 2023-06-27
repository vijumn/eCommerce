using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ryzen.Shop.Catalog.Domain;

namespace Ryzen.Shop.Catalog.Persistence
{
    public partial class CatalogContext : DbContext, Ryzen.Shop.Catalog.Application.Data.ICatalogContext
    {
        public CatalogContext(DbContextOptions<CatalogContext> options) : base(options)
        {
        }
        
        public DbSet<Product> Products { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<PromotionType> PromotionTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Product>()
            //    .HasOne(p => p.ProductPromotion)
            //    .WithMany(b => b.Products)
            //    .HasForeignKey(p => p.PromotionID);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogContext).Assembly);
                OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
