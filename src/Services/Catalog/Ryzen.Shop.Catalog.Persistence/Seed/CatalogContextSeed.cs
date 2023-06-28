
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
            new() { Type =  PromotionType.ItemDiscount,Name="ItemDiscount", DiscountAmount=2.0m  },
            new() { Type =  PromotionType.ItemDiscount,Name="ItemDiscount", DiscountPercentage=10.00m  },
            new() { Type =  PromotionType.SecondOneDiscountPercentage,Name="SecondOneDiscountPercentage", DiscountPercentage=20m  },
            new() { Type =  PromotionType.ItemDiscount,Name="ItemDiscount", DiscountAmount=2.00m  },
            new() { Type = PromotionType.GetOneFree, Name="GetOneFree", GetOneFree= true },

            new() { Type =  PromotionType.MinimumSpend,Name="MinimumSpend", DiscountAmount=5m, MinimumSpendAmount=50m  },
            new() { Type =  PromotionType.TrollyDiscount,Name="TrollyDiscount", DiscountAmount=.1m  },

        };
    }

   
    private IEnumerable<Product> GetCatalogPrducts()
    {
        return new List<Product>()
        {
            new Product(){ Name= "Jacob's Creek Classic Sauvignon Blanc", PromotionID=null, Description="The Jacob's Creek Sauvignon Blanc is a classic Sauvignon Blanc from the reknowned Jacob's Creek. The nose is lifted and fragrant with passionfruit and varietal asparagus. The palate offers tropical and herbacious flavours and a delightful crisp finish.", Price=7.95m, Picture="jacob-sauvington-blanc.png" },
            new Product(){ Name= "Tread Softly Pinot Noir", PromotionID=null, Description="Tread Softly is a progressively eco-conscious drinks brand, that plants an Australian native tree for every 6 bottles sold. The brand has recently planted the one-millionth tree in the 'Tread Softly Forest' and is celebrating with limited edition native Australian bird artwork labels available in-store now.", Price=13.60m, Picture="Tread-Softly-Pinot-Noir.png" },
            new Product(){ Name= "Coopers Original Pale Ale Bottle", PromotionID=null, Description="Coopers Original Pale\r\nAle Bottle.", Price=4.69m, Picture="Coopers.png" },
            new Product(){ Name= "Marlborough Sounds Pinot Noir bottle", PromotionID=1, Description="Marlborough Sounds Pinot Noir bottle.", Price=19.99m, Picture="Marlborough.png" },
            new Product(){ Name= "Pepperjack Barossa\r\nShiraz", PromotionID=2, Description="Pepperjack Barossa\r\nShiraz", Price=25.00m, Picture="Pepperjack.png" },
            new Product(){ Name= "Baily & Baily\r\nQueen Bee Sticky", PromotionID=4, Description="Baily & Baily\r\nQueen Bee Sticky", Price=12.00m, Picture="Baily.png" },
            new Product(){ Name= "Victoria Bitter", PromotionID=4, Description="Victoria Bitter", Price=21.49m, Picture="Victoria.png" },
            new Product(){ Name= "Crown Lager", PromotionID=null, Description="Crown Lager", Price=21.49m, Picture="Crown.png" },
            new Product(){ Name= "Coopers", PromotionID=5, Description="Coopers", Price=20.49m, Picture="Coopers.png" },
            new Product(){ Name= "Tooheys Extra Dry", PromotionID=null, Description="Tooheys Extra Dry", Price=19.99m, Picture="Tooheys.png" },

        };
    }
 
}
