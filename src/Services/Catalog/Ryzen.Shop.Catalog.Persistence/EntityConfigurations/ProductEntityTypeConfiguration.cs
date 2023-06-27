using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ryzen.Shop.Catalog.Domain;

namespace Ryzen.Shop.Catalog.Persistence.EntityConfigurations;

class ProductEntityTypeConfiguration
    : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(cb => cb.ProductID);

        builder.Property(cb => cb.ProductID)
            .UseHiLo("Product_hilo")
            .IsRequired();

        builder.Property(cb => cb.Name)
            .HasMaxLength(500);
        builder.Property(cb => cb.Description)
       .HasMaxLength(4000);

        builder.Property(cb => cb.Picture)
       .HasMaxLength(500);
    }
}
