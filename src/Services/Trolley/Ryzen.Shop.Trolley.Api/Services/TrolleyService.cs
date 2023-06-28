using System.Net.Http;
using Ryzen.Shop.Trolley.Api.Business;
using Ryzen.Shop.Trolley.Api.Model;

namespace Ryzen.Shop.Trolley.Api.Services
{
    public class TrolleyService : ITrolleyService
    {
        private readonly IPromotionsService _promotionsService;
        private readonly IDiscountEngine _discountEngine;
        public TrolleyService(IPromotionsService promotionsService, IDiscountEngine discountEngine)
        {
            _promotionsService = promotionsService;
            _discountEngine = discountEngine;
        }

        public async Task<CustomerTrolley> GetTrolley(string trolleyId)
        {
            return new CustomerTrolley();
        }
        public async Task<CustomerTrolley> UpdateTrolley(string trolleyId,CustomerTrolley customerTrolley)
        {
            var ids = customerTrolley.Items.Select(x => x.ProductId).ToList();
            var promotions =await _promotionsService.GetPromotions(ids);
            var trolley = await _discountEngine.ApplyDiscount(promotions, customerTrolley);

            return customerTrolley;


        }

    }
}
