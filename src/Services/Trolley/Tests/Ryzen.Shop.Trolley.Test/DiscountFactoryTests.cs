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
            var promotion = DiscountFactory.CreatePromotion(promotionType, data);

            Assert.NotNull(promotion);
            Assert.IsType(expectedType, promotion);
        }

        public static IEnumerable<object[]> GetData()
        {
            dynamic itempromotion = new ExpandoObject();
            itempromotion.ProductId = 1;
            itempromotion.AmountOff = 10m;
            itempromotion.PercentageOff = 0.0m;
            dynamic trolleyPromotion = new ExpandoObject();
            trolleyPromotion.ProductId = 1;
            trolleyPromotion.MinimumSpendAmount = 10m;
            trolleyPromotion.MinimumSpendDiscountAmount = 5.0m;

            var allData = new List<object[]>
            {   
                new object[] { "ItemPromotion", typeof(ItemPromotion),itempromotion },
                new object[] { "TrolleyPromotion", typeof(TrolleyPromotion),trolleyPromotion },
            };

            return allData;
        }
    }
}
