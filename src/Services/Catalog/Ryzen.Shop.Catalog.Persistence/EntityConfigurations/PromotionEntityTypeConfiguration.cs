using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ryzen.Shop.Catalog.Domain;
using Ryzen.Shop.Catalog.Persistence;

namespace Microsoft.eShopOnContainers.Services.Ordering.Infrastructure.EntityConfigurations;

class PromotionTypeTypeEntityTypeConfiguration
    : IEntityTypeConfiguration<PromotionType>
{
    public void Configure(EntityTypeBuilder<PromotionType> cardTypesConfiguration)
    {
        cardTypesConfiguration.ToTable("PromotionTypes");

        cardTypesConfiguration.HasKey(ct => ct.Id);

        cardTypesConfiguration.Property(ct => ct.Id)
            .HasDefaultValue(1)
            .ValueGeneratedNever()
            .IsRequired();

        cardTypesConfiguration.Property(ct => ct.Name)
            .HasMaxLength(200)
            .IsRequired();
    }
}
