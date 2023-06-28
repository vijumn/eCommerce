using FluentAssertions;
using Ryzen.Shop.Trolley.Api.Business;
using Ryzen.Shop.Trolley.Api.Model;
using Ryzen.Shop.Trolley.Api.Services;

namespace Ryzen.Shop.Trolley.Test
{
    public class DiscountEngineFunctionalTests
    {
        [Fact]
        public async Task ApplyDiscount_ShouldApplyPromotionsInOrder()
        {
            // Arrange
            var discountEngine = new DiscountEngine();
            var promotions = new List<Promotion> {
            new Promotion { ProductId=1, DiscountAmount=2, Type=PromotionType.ItemDiscount  },
            new Promotion { ProductId=2, DiscountPercentage=2, Type=PromotionType.ItemDiscount  },
            new Promotion {  DiscountAmount=10,MinimumSpend=50,  Type=PromotionType.MinimumSpend  },
        };
            var customerTrolley = new CustomerTrolley
            {
                CustomerId = "1",
                Items = new List<TrolleyItem>
                  {
                      new TrolleyItem { ProductId=1, Quantity=1, UnitPrice=10 },
                      new TrolleyItem { ProductId=2, Quantity=1, UnitPrice=20 },
                      new TrolleyItem { ProductId=3, Quantity=1, UnitPrice=30 },
                  }
            };

            var resultTrolley = new CustomerTrolley
            {
                CustomerId = "1",
                Items = new List<TrolleyItem>
                {
                      new TrolleyItem { ProductId=1, Quantity=1, UnitPrice=10, UnitPriceSale=8 },
                      new TrolleyItem { ProductId=2, Quantity=1, UnitPrice=20, UnitPriceSale=19.6m },
                      new TrolleyItem { ProductId=3, Quantity=1, UnitPrice=30, UnitPriceSale=null },
                },
                CartDiscount = 10m,
            };

            DiscountFactory discountFactory = new DiscountFactory();

            // Act
            var result = await discountEngine.ApplyDiscount(promotions, customerTrolley);

            // Assert
            result.Should().BeEquivalentTo(resultTrolley);
        }

        /*
         * User Story 1:
            As a customer I want to add / remove items to the trolley so that I can purchase the drinks I
            want
            Acceptance Criteria:
            1. Customers can add the same i tem more than once. For MVP, there i s no upper l imit
            on the number of i tems or quantity they add
            2. When customer adds an i tem to the trolley, they can see the count of i tems
            incremented
            a. Given customer has no i tem i n the trolley
            b. When they add the first i tem
            c. Then the trolley count shows 1
            3. When Customer removes i tem from the trolley, the count of i tems i s decremented
            a. Given customer has 1 i tem i n the trolley
            b. When they remove that i tem from the trolley
            c. Then the trolley count shows
         *
         */

        [Fact]
        public async Task Should_Return_Trolley_With_Items_Count_Total()
        {
            // Arrange
            var discountEngine = new DiscountEngine();
            var promotions = new List<Promotion>
            {
            };
            var customerTrolley = new CustomerTrolley
            {
                CustomerId = "1",
                Items = new List<TrolleyItem>
                {
                      new TrolleyItem { ProductId=1, Quantity=1, UnitPrice=10 },
                      new TrolleyItem { ProductId=2, Quantity=1, UnitPrice=20 },
                      new TrolleyItem { ProductId=3, Quantity=1, UnitPrice=30 },
                  }
            };

            var resultTrolley = new CustomerTrolley
            {
                CustomerId = "1",
                Items = new List<TrolleyItem>
                {
                      new TrolleyItem { ProductId=1, Quantity=1, UnitPrice=10, UnitPriceSale=null },
                      new TrolleyItem { ProductId=2, Quantity=1, UnitPrice=20, UnitPriceSale=null },
                      new TrolleyItem { ProductId=3, Quantity=1, UnitPrice=30, UnitPriceSale=null },
                },
                CartDiscount = 0.0m,
            };

            DiscountFactory discountFactory = new DiscountFactory();

            // Act
            var result = await discountEngine.ApplyDiscount(promotions, customerTrolley);

            // Assert
            result.Should().BeEquivalentTo(resultTrolley);
            result.ItemsCount.Should().Be(3);
        }

        /*
         * User Story 2:
            As a customer when I view trolley, I want to see all the i tems I have added and the price of
            each i tem i n the trolley so that I can see what I am paying for each i tem
            1. For each i tem show the product name and sale price
            Given “Jacob's Creek Classic Sauvignon Blanc” has a sale price of $7.95 i s i n the
            trolley
            When I view the trolley
            Then I want to see a sale price of Jacob's Creek Classic Sauvignon Blanc and $7.95
        */

        [Fact]
        public async Task Should_Return_Trolley_With_Items_Name_SalePrice()
        {
            // Arrange
            var discountEngine = new DiscountEngine();
            var promotions = new List<Promotion>
            {
            };
            var customerTrolley = new CustomerTrolley
            {
                CustomerId = "1",
                Items = new List<TrolleyItem>
                {
                      new TrolleyItem { ProductId=1, Quantity=1, UnitPrice=7.95m, ProductName="Jacob's Creek Classic Sauvignon Blanc"  },
                  }
            };

            var resultTrolley = new CustomerTrolley
            {
                CustomerId = "1",
                Items = new List<TrolleyItem>
                {
                      new TrolleyItem { ProductId=1, Quantity=1, UnitPrice=7.95m, UnitPriceSale=null, ProductName="Jacob's Creek Classic Sauvignon Blanc" },
                },
                CartDiscount = 0.0m,
            };

            DiscountFactory discountFactory = new DiscountFactory();
            // Act
            var result = await discountEngine.ApplyDiscount(promotions, customerTrolley);

            // Assert
            result.Should().BeEquivalentTo(resultTrolley);
            result.ItemsCount.Should().Be(1);
        }

        /*
         * User Story 3:
        As a customer when I view trolley, I want to see the total price of my trolley so that I know
        what I am paying
        1. Add up the i tem price and display total price
        Given I have i tems “Jacob's Creek Classic Sauvignon Blanc” with a sale price of
        $7.95, “Tread Softly Pinot Noir” with sale price of $13.60 and “Coopers Original Pale
        Ale Bottle” with sale price of $4.69 i n the trolley
        When I view the trolley
        Then I want to see a total price of $26.24
        */

        [Fact]
        public async Task Should_Return_Trolley_With_TotalPrice()
        {
            // Arrange
            var discountEngine = new DiscountEngine();
            var promotions = new List<Promotion>
            {
            };
            var customerTrolley = new CustomerTrolley
            {
                CustomerId = "1",
                Items = new List<TrolleyItem>
            {
                      new TrolleyItem { ProductId=1, Quantity=1, UnitPrice=7.95m, ProductName="Jacob's Creek Classic Sauvignon Blanc"  },
                      new TrolleyItem { ProductId=2, Quantity=1, UnitPrice=13.60m, ProductName="Tread Softly Pinot Noir"  },
                      new TrolleyItem { ProductId=3, Quantity=1, UnitPrice=4.69m, ProductName="Coopers Original Pale Ale Bottle"  },
                  }
            };

            var resultTrolley = new CustomerTrolley
            {
                CustomerId = "1",
                Items = new List<TrolleyItem>
            {
                      new TrolleyItem { ProductId=1, Quantity=1, UnitPrice=7.95m, UnitPriceSale=null, ProductName="Jacob's Creek Classic Sauvignon Blanc" },
                      new TrolleyItem { ProductId=2, Quantity=1, UnitPrice=13.60m, UnitPriceSale=null, ProductName="Tread Softly Pinot Noir" },
                      new TrolleyItem { ProductId=3, Quantity=1, UnitPrice=4.69m, UnitPriceSale=null, ProductName="Coopers Original Pale Ale Bottle" },
                },
                CartDiscount = 0.0m,
            };

            DiscountFactory discountFactory = new DiscountFactory();
            // Act
            var result = await discountEngine.ApplyDiscount(promotions, customerTrolley);

            // Assert
            result.Should().BeEquivalentTo(resultTrolley);
            result.ItemsCount.Should().Be(3);
            result.Total.Should().Be(26.24m);
        }

        /*
         *         * User Story 4:
         *         As a customer when I view trolley, I want to see the discounted price i f there are any
                    discounts for each i tem i n the trolley so that I know what I am paying for each i tem after the
                    discount
                    1. For each i tem, show discounted price along with sale price
                    Given trolley has “Marlborough Sounds Pinot Noir bottle” with sale price of $19.99
                    and a product promotion is running that gives $2.00 off and “Pepperjack Barossa
                    Shiraz” has a sale price of $25.00 and a product promotion gives 10% off
                    When I view the trolley
                    Then I want to see against the first i tem sale price of $19.99 and a discounted price
                    of $17.99
                    And against i tem 2, a sale price of $25.00 and a discounted price of $22.50
        */

        [Fact]
        public async Task Should_Return_Trolley_With_Items_SalePrice_DiscountedPrice()
        {
            // Arrange
            var discountEngine = new DiscountEngine();
            var promotions = new List<Promotion>
            {
                 new Promotion { ProductId=1, DiscountAmount=2, Type=PromotionType.ItemDiscount  },
                 new Promotion { ProductId=2, DiscountPercentage=10m, Type=PromotionType.ItemDiscount  },
            };
            var customerTrolley = new CustomerTrolley
            {
                CustomerId = "1",
                Items = new List<TrolleyItem>
                {
                      new TrolleyItem { ProductId=1, Quantity=1, UnitPrice=19.99m, ProductName="Marlborough Sounds Pinot Noir bottle"  },
                      new TrolleyItem { ProductId=2, Quantity=1, UnitPrice=25.00m, ProductName="Pepperjack Barossa Shiraz"  },
                  }
            };

            var resultTrolley = new CustomerTrolley
            {
                CustomerId = "1",
                Items = new List<TrolleyItem>
                {
                      new TrolleyItem { ProductId=1, Quantity=1, UnitPrice=19.99m, UnitPriceSale=17.99m, ProductName="Marlborough Sounds Pinot Noir bottle" },
                      new TrolleyItem { ProductId=2, Quantity=1, UnitPrice=25.00m, UnitPriceSale=22.50m, ProductName="Pepperjack Barossa Shiraz" },
                },
                CartDiscount = 0.0m,
            };

            DiscountFactory discountFactory = new DiscountFactory();
            // Act
            var result = await discountEngine.ApplyDiscount(promotions, customerTrolley);

            // Assert
            result.Should().BeEquivalentTo(resultTrolley);
            result.ItemsCount.Should().Be(2);
            result.Total.Should().Be(40.49m);
        }

        /*
        * User Story 5:
        As a customer when I view trolley, I want to see the total price of my trolley and the
        discounted price after all discount have been applied so that I know what I am paying and
        what benefit I am getting
        1. Apply all product l evel discounts, then apply trolley l evel discounts, subtract this from
        the total sale price to get the discounted priceGiven trolley has one “Marlborough Sounds Pinot Noir bottle” with sale price of
        $19.99 and a product promotion i s running that gives $2.00 off, two “Baily & Baily
        Queen Bee Sticky” which has a sale price of $12.00 and a promotion of buy one and
        get 20% off the second
        When I view the trolley
        Then I want to see a total sale price of $43.99 and discounted price of $39.59
        */
        [Fact]
        public async Task Should_Return_Trolley_With_TotalPrice_DiscountedPrice()
        {
            // Arrange
            var discountEngine = new DiscountEngine();
            var promotions = new List<Promotion>
            {
                 new Promotion { ProductId=1, DiscountAmount=2m, Type=PromotionType.ItemDiscount  },
                 new Promotion { ProductId=2, DiscountPercentage=20m, Type=PromotionType.SecondOneDiscountPercentage  },
            };
            var customerTrolley = new CustomerTrolley
            {
                CustomerId = "1",
                Items = new List<TrolleyItem>
                {
                      new TrolleyItem { ProductId=1, Quantity=1, UnitPrice=19.99m, ProductName="Marlborough Sounds Pinot Noir bottle"  },
                      new TrolleyItem { ProductId=2, Quantity=2, UnitPrice=12.00m, ProductName="Baily & Baily Queen Bee Sticky"  },
                  }
            };

            var resultTrolley = new CustomerTrolley
            {
                CustomerId = "1",
                Items = new List<TrolleyItem>
                {
                      new TrolleyItem { ProductId=1, Quantity=1, UnitPrice=19.99m, UnitPriceSale=17.99m, ProductName="Marlborough Sounds Pinot Noir bottle" },
                      new TrolleyItem { ProductId=2, Quantity=2, UnitPrice=12.00m, UnitPriceSale=10.8m, ProductName="Baily & Baily Queen Bee Sticky" },
                },
                CartDiscount = 0.0m,
            };

            DiscountFactory discountFactory = new DiscountFactory();
            // Act
            var result = await discountEngine.ApplyDiscount(promotions, customerTrolley);

            // Assert
            result.Should().BeEquivalentTo(resultTrolley);
            result.ItemsCount.Should().Be(3);
            result.Total.Should().Be(39.59m);
        }
        /*
      * User Story 5:
      As a customer when I view trolley, I want to see the total price of my trolley and the
      discounted price after all discount have been applied so that I know what I am paying and
      what benefit I am getting
      1. Apply all product l evel discounts, then apply trolley l evel discounts, subtract this from
      the total sale price to get the discounted priceGiven trolley has one “Marlborough Sounds Pinot Noir bottle” with sale price of
      $19.99 and a product promotion i s running that gives $2.00 off, two “Baily & Baily
      Queen Bee Sticky” which has a sale price of $12.00 and a promotion of buy one and
      get 20% off the second
      When I view the trolley
      Then I want to see a total sale price of $43.99 and discounted price of $39.59
      */
        [Fact]
        public async Task Should_Return_Trolley_With_TotalPrice_DiscountedPrice_With_DifferentQuantity()
        {
            // Arrange
            var discountEngine = new DiscountEngine();
            var promotions = new List<Promotion>
            {
                 new Promotion { ProductId=1, DiscountAmount=2m, Type=PromotionType.ItemDiscount  },
                 new Promotion { ProductId=2, DiscountPercentage=20m, Type=PromotionType.SecondOneDiscountPercentage  },
            };
            var customerTrolley = new CustomerTrolley
            {
                CustomerId = "1",
                Items = new List<TrolleyItem>
                {
                      new TrolleyItem { ProductId=1, Quantity=1, UnitPrice=19.99m, ProductName="Marlborough Sounds Pinot Noir bottle"  },
                      new TrolleyItem { ProductId=2, Quantity=6, UnitPrice=12.00m, ProductName="Baily & Baily Queen Bee Sticky"  },
                  }
            };

            var resultTrolley = new CustomerTrolley
            {
                CustomerId = "1",
                Items = new List<TrolleyItem>
                {
                      new TrolleyItem { ProductId=1, Quantity=1, UnitPrice=19.99m, UnitPriceSale=17.99m, ProductName="Marlborough Sounds Pinot Noir bottle" },
                      new TrolleyItem { ProductId=2, Quantity=6, UnitPrice=12.00m, UnitPriceSale=10.8m, ProductName="Baily & Baily Queen Bee Sticky" },
                },
                CartDiscount = 0.0m,
            };

            DiscountFactory discountFactory = new DiscountFactory();
            // Act
            var result = await discountEngine.ApplyDiscount(promotions, customerTrolley);

            // Assert
            result.Should().BeEquivalentTo(resultTrolley);
            result.ItemsCount.Should().Be(7);
            result.Total.Should().Be(82.79m);
        }

        /* Final Test Data
         * Product Sale Price
            Victoria Bitter $21.49
            Crown Lager $22,99
            Coopers $20.49
            Tooheys Extra Dry $19.99

        Product Promotions

        Name Definition Type
        Current month special on VB $2.00 off Product
        BOGOF on Coopers Buy one and get one free Trolley
        Spend and save Spend $50 and $5 off the total Trolley

        */

        [Fact]
        public async Task Should_Return_Trolley_With_TotalPrice_DiscountedPrice_With_Final_Test_Data()
        {
            // Arrange
            var discountEngine = new DiscountEngine();
            var promotions = new List<Promotion>
            {
                 new Promotion { ProductId=1, DiscountAmount=2m, Type=PromotionType.ItemDiscount  },
                 new Promotion { ProductId=3, GetOneFree=true , Type=PromotionType.GetOneFree  },
                 new Promotion {  DiscountAmount=5m, Type=PromotionType.MinimumSpend  },
            };
            var customerTrolley = new CustomerTrolley
            {
                CustomerId = "1",
                Items = new List<TrolleyItem>
                {
                      new TrolleyItem { ProductId=1, Quantity=1, UnitPrice=21.49m, ProductName="Victoria Bitter"  },
                      new TrolleyItem { ProductId=2, Quantity=1, UnitPrice=22.99m, ProductName="Crown Lager"  },
                      new TrolleyItem { ProductId=3, Quantity=2, UnitPrice=20.49m, ProductName="Coopers"  },
                      new TrolleyItem { ProductId=4, Quantity=1, UnitPrice=19.99m, ProductName="Tooheys Extra Dry"  },
                  }
            };

            var resultTrolley = new CustomerTrolley
            {
                CustomerId = "1",
                Items = new List<TrolleyItem>
                {
                      new TrolleyItem { ProductId=1, Quantity=1, UnitPrice=21.49m, UnitPriceSale=19.49m, ProductName="Victoria Bitter" },
                      new TrolleyItem { ProductId=2, Quantity=1, UnitPrice=22.99m, UnitPriceSale=null, ProductName="Crown Lager" },
                      new TrolleyItem { ProductId=3, Quantity=2, UnitPrice=20.49m, UnitPriceSale=10.245m, ProductName="Coopers" },
                      new TrolleyItem { ProductId=4, Quantity=1, UnitPrice=19.99m, UnitPriceSale=null, ProductName="Tooheys Extra Dry" },
                },
                CartDiscount = 5.0m,
            };

            DiscountFactory discountFactory = new DiscountFactory();
            // Act
            var result = await discountEngine.ApplyDiscount(promotions, customerTrolley);

            // Assert
            result.Should().BeEquivalentTo(resultTrolley);
        }


        /* Final Test Data
         *         Product Sale Price
                Victoria Bitter $21.49
                Crown Lager $22,99
                Coopers $20.49
                Tooheys Extra Dry $19.99
        */
        [Fact]
        public async Task Should_Return_Trolley_With_TotalPrice_DiscountedPrice_With_Final_Test_Data_DifferentQuantity()
        {
            // Arrange
            var discountEngine = new DiscountEngine();
            var promotions = new List<Promotion>
            {
                 new Promotion { ProductId=1, DiscountAmount=2m, Type=PromotionType.ItemDiscount  },
                // new Promotion { ProductId=3, GetOneFree=true , Type=PromotionType.GetOneFree  },
                 new Promotion { ProductId=3, DiscountPercentage=100m , Type=PromotionType.SecondOneDiscountPercentage  },
                 new Promotion {  DiscountAmount=5m, Type=PromotionType.MinimumSpend  },
            };
            var customerTrolley = new CustomerTrolley
            {
                CustomerId = "1",
                Items = new List<TrolleyItem>
                {
                      new TrolleyItem { ProductId=1, Quantity=1, UnitPrice=21.49m, ProductName="Victoria Bitter"  },
                      new TrolleyItem { ProductId=2, Quantity=1, UnitPrice=22.99m, ProductName="Crown Lager"  },
                      new TrolleyItem { ProductId=3, Quantity=5, UnitPrice=20.49m, ProductName="Coopers"  },
                      new TrolleyItem { ProductId=4, Quantity=1, UnitPrice=19.99m, ProductName="Tooheys Extra Dry"  },
                  }
            };

            var resultTrolley = new CustomerTrolley
            {
                CustomerId = "1",
                Items = new List<TrolleyItem>
                {
                      new TrolleyItem { ProductId=1, Quantity=1, UnitPrice=21.49m, UnitPriceSale=19.49m, ProductName="Victoria Bitter" },
                      new TrolleyItem { ProductId=2, Quantity=1, UnitPrice=22.99m, UnitPriceSale=null, ProductName="Crown Lager" },
                      new TrolleyItem { ProductId=3, Quantity=5, UnitPrice=20.49m, UnitPriceSale=12.294m, ProductName="Coopers" },
                      new TrolleyItem { ProductId=4, Quantity=1, UnitPrice=19.99m, UnitPriceSale=null, ProductName="Tooheys Extra Dry" },
                },
                CartDiscount = 5.0m,
            };

            DiscountFactory discountFactory = new DiscountFactory();
            // Act
            var result = await discountEngine.ApplyDiscount(promotions, customerTrolley);

            // Assert
            result.Should().BeEquivalentTo(resultTrolley);
        }
    }

}
