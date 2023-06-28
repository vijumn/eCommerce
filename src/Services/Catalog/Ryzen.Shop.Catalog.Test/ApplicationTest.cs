using Microsoft.EntityFrameworkCore;
using Moq;
using Ryzen.Shop.Catalog.Api.Controllers;
using Ryzen.Shop.Catalog.Domain;
using Ryzen.Shop.Catalog.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Ryzen.Shop.Catalog.Test
{
    public  class ApplicationTest
    {
        private readonly DbContextOptions<CatalogContext> _dbOptions;
        public ApplicationTest()
        { 
            _dbOptions = new DbContextOptionsBuilder<CatalogContext>()
                .UseInMemoryDatabase(databaseName: "Catalog")
                .Options;

            using var dbContext = new CatalogContext(_dbOptions);
            dbContext.AddRange(GetFakeProducts());
            dbContext.AddRange(GetCatalogPromotionTypes());
            dbContext.AddRange(GetFakePromotions());
            dbContext.SaveChanges();

        }

        [Fact]
        public async Task Get_Products_Success()
        {
            //Arrange
           

            var expectedItemsInPage = 2;
            var expectedTotalItems = 6;

            var catalogContext = new CatalogContext(_dbOptions);


            //Act
            //var productsController = new ProductsController();
            //var actionResult = await productsController.GetAll();

            //Assert
        }
        private List<Product> GetFakeProducts()
        {
            return new List<Product>()
        {
            new()
            {
                ProductID = 1,
                Name = "fakeItemA",
                Description = "FakefakeItemA",
                PromotionID = 1,
                Picture = "fakeItemA.png",
                Price = 18,
            },
            new()
            {
              ProductID = 2,
                Name = "fakeItemB",
                Description = "FakefakeItemB",
                PromotionID = 1,
                Picture = "fakeItemB.png",
                Price = 25.89m,

            },
            new()
            {
               ProductID = 3,
                Name = "fakeItemC",
                Description = "FakefakeItemC",
                PromotionID = 1,
                Picture = "fakeItemC.png",
                Price = 34.89m,
            },
          
        };
        }

        private List<Promotion> GetFakePromotions()
        {
            return new List<Promotion>()
            {
            new()
            {
                PromotionID = 1,
                Name = "fakePromotionA",
                Type = PromotionType.ItemDiscount,
                DiscountAmount = 10,
                DiscountPercentage = 0,
                MinimumSpendAmount = 0,
                GetOneFree = false,
                SecondOneDiscountPercentage = 0,
            },
            new()
            {
                PromotionID = 2,
                Name = "fakePromotionB",
                Type = PromotionType.ItemDiscount,
                DiscountAmount = 0,
                DiscountPercentage = 10,
                MinimumSpendAmount = 0,
                GetOneFree = false,
                SecondOneDiscountPercentage = 0,
            },
            new()
            {
                PromotionID = 3,
                Name = "fakePromotionC",
                Type =PromotionType.TrollyDiscount,
                DiscountAmount = 0,
                DiscountPercentage = 0,
                MinimumSpendAmount = 100,
                GetOneFree = false,
                SecondOneDiscountPercentage = 0,
            },
            new()
            {
                PromotionID = 4,
                Name = "fakePromotionD",
                Type =PromotionType.GetOneFree,
                DiscountAmount = 0,
                DiscountPercentage = 0,
                MinimumSpendAmount = 0,
                GetOneFree = true,
                SecondOneDiscountPercentage = 0,
            },
            new()
            {
                PromotionID = 5,
                Name = "fakePromotionE",
                Type = PromotionType.SecondOneDiscountPercentage,
                DiscountAmount = 0,
                DiscountPercentage = 0,
                MinimumSpendAmount = 0,
                GetOneFree = false,
                SecondOneDiscountPercentage = 50,
            },
        };
        }

        private IEnumerable<PromotionType> GetCatalogPromotionTypes()
        {
            return Ryzen.Shop.Shared.Enumeration.GetAll<PromotionType>();

        }

    }
}
