using Ryzen.Shop.Trolley.Api.Services;

namespace Ryzen.Shop.Trolley.Api.Business
{
    public class DiscountEngine : IDiscountEngine
    {
        private readonly IList<IPromotion> _promotions;

        public DiscountEngine()
        {
            _promotions = new List<IPromotion>();
        }

        public async Task ApplyDiscount(List<Promotion> promotions, CustomerTrolley customerTrolley)
        {

            foreach (var data in promotions)
            {
                var promotion = DiscountFactory.CreatePromotion(data.Type.Name, data);

                if (promotion != null)
                {
                    _promotions.Add(promotion);
                }
            }

            foreach (var promotion in _promotions.OrderBy(p=>p.Order))
            {
                promotion.Apply(customerTrolley);
            }
            return;
        }


    }
}
