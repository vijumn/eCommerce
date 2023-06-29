using System.Dynamic;
using Ryzen.Shop.Trolley.Api.Business;
using Ryzen.Shop.Trolley.Api.Model;

namespace Ryzen.Shop.Trolley.Test
{
    public class DiscountFactoryTests
    {
        [Theory]
        [MemberData(nameof(GetData))]
        public void CreatePromotion_ShouldReturnCorrectType(string promotionType, Type expectedType, dynamic data)
        {
            var promotion = new DiscountFactory().CreatePromotion(promotionType, data);

            Assert.NotNull(promotion);
            Assert.IsType(expectedType, promotion);
        }

        public static IEnumerable<object[]> GetData()
        {
            dynamic itempromotion = new ExpandoObject();
            itempromotion.ProductId = 1;
            itempromotion.DiscountAmount = 10m;
            itempromotion.DiscountPercentage = 0.0m;
           
            dynamic trolleyPromotion = new ExpandoObject();
            trolleyPromotion.ProductId = 1;
            trolleyPromotion.MinimumSpend= 10m;
            trolleyPromotion.DiscountAmount = 5.0m;

            dynamic bogof = new ExpandoObject();
            bogof.ProductId = 1;
            bogof.GetOneFree = true;

            dynamic secondOneDiscountPercentage = new ExpandoObject();
            secondOneDiscountPercentage.ProductId = 1;
            secondOneDiscountPercentage.DiscountPercentage = 10.0m;

            var allData = new List<object[]>
            {   
                new object[] { "ItemDiscount", typeof(ItemDiscount),itempromotion },
                new object[] { "MinimumSpend", typeof(MinimumSpend),trolleyPromotion },
                new object[] { "GetOneFree", typeof(GetOneFree),bogof },
                new object[] { "SecondOneDiscountPercentage", typeof(SecondOneDiscountPercentage),secondOneDiscountPercentage },
            };

            return allData;
        }
    }
}
