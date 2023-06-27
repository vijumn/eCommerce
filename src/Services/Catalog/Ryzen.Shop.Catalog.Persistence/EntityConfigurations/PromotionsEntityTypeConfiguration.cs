using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ryzen.Shop.Catalog.Domain;

namespace Ryzen.Shop.Catalog.Persistence.EntityConfigurations;

class PromotionsEntityTypeConfiguration
    : IEntityTypeConfiguration<Promotion>
{
    public void Configure(EntityTypeBuilder<Promotion> builder)
    {
        builder.ToTable("Promotions");

        builder.HasKey(ci => ci.PromotionID);

        builder.Property(ci => ci.PromotionID)
            .UseHiLo("Promotions_hilo")
            .IsRequired();

        builder.Property(cb => cb.Name)
            .HasMaxLength(500);
    }
}
