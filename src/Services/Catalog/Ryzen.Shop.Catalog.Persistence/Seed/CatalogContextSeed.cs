
using Ryzen.Shop.Catalog.Domain;
using Ryzen.Shop.Catalog.Persistence;

public class CatalogContextSeed
{
    public async Task SeedAsync(CatalogContext context )
    {

            if (!context.PromotionTypes.Any())
            {
            await context.PromotionTypes.AddRangeAsync(GetCatalogPromotionTypes());
                    

                await context.SaveChangesAsync();
            }


            if (!context.Promotions.Any())
            {
                await context.Promotions.AddRangeAsync(GetCatalogPromotions());

                await context.SaveChangesAsync();

            }
            if (!context.Products.Any())
            {
                await context.Products.AddRangeAsync(GetCatalogPrducts());

                await context.SaveChangesAsync();
            }

    }

   


    private IEnumerable<PromotionType> GetCatalogPromotionTypes()
    {
    return Ryzen.Shop.Shared.Enumeration.GetAll<PromotionType>();
      
    }

   



    private IEnumerable<Promotion> GetCatalogPromotions()
    {
        return new List<Promotion>()
        {
            new() { Type = PromotionType.GetOneFree, Name="GetOneFree" },
            new() { Type =  PromotionType.ItemDiscount,Name="ItemDiscount", DiscountAmount=7.5m  },
            new() { Type =  PromotionType.ItemDiscount,Name="ItemDiscount", DiscountAmount=13.60m  },
            new() { Type =  PromotionType.ItemDiscount,Name="ItemDiscount", DiscountAmount=4.69m  },
            new() { Type =  PromotionType.ItemDiscount,Name="ItemDiscount", DiscountAmount=.1m  },
            new() { Type =  PromotionType.TrollyDiscount,Name="TrollyDiscount", DiscountAmount=.1m  },
            new() { Type =  PromotionType.SecondOneDiscountPercentage,Name="SecondOneDiscountPercentage", DiscountAmount=.2m  },
            new() { Type =  PromotionType.MinimumSpend,Name="MinimumSpend", DiscountAmount=5m, MinimumSpendAmount=50m  },
        };
    }

   
    private IEnumerable<Product> GetCatalogPrducts()
    {
        return new List<Product>()
        {
            new Product(){ Name= "Jacob's Creek Classic Sauvignon Blanc", PromotionID=2, Description="The Jacob's Creek Sauvignon Blanc is a classic Sauvignon Blanc from the reknowned Jacob's Creek. The nose is lifted and fragrant with passionfruit and varietal asparagus. The palate offers tropical and herbacious flavours and a delightful crisp finish.", Price=8.3m, Picture="jacob-sauvington-blanc.png" },
            new Product(){ Name= "Tread Softly Pinot Noir", PromotionID=3, Description="Tread Softly is a progressively eco-conscious drinks brand, that plants an Australian native tree for every 6 bottles sold. The brand has recently planted the one-millionth tree in the 'Tread Softly Forest' and is celebrating with limited edition native Australian bird artwork labels available in-store now.", Price=13.99m, Picture="Tread-Softly-Pinot-Noir.png" },
            new Product(){ Name= "Coopers Original Pale Ale Bottle", PromotionID=4, Description="Coopers Original Pale\r\nAle Bottle.", Price=5m, Picture="Coopers.png" },

        };
    }
 
}
