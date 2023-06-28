using System.Net.Http;
using Ryzen.Shop.Trolley.Api.Business;
using Ryzen.Shop.Trolley.Api.Model;

namespace Ryzen.Shop.Trolley.Api.Services
{
    public class TrolleyService : ITrolleyService
    {
        private readonly IPromotionsService _promotionsService;
        private readonly IDiscountEngine _discountEngine;
        private readonly IProductsService _productsService;
        public TrolleyService(IPromotionsService promotionsService, IDiscountEngine discountEngine, IProductsService productsService)
        {
            _promotionsService = promotionsService;
            _discountEngine = discountEngine;
            _productsService = productsService;

        }

        public async Task<CustomerTrolley> GetTrolley(string trolleyId)
        {
            return new CustomerTrolley();
        }
        public async Task<CustomerTrolley> UpdateTrolley(string trolleyId,CustomerTrolley customerTrolley)
        {
            var ids = customerTrolley.Items.Select(x => x.ProductId).ToList();
            var promotions =await _promotionsService.GetPromotions(ids);
            var products = await _productsService.GetProducts(ids);
            EnrichTrolley(customerTrolley, products);
            var trolley = await _discountEngine.ApplyDiscount(promotions, customerTrolley);

            return customerTrolley;

        }
        private void EnrichTrolley(CustomerTrolley customerTrolley, List<Product> products)
        {
              foreach(var item in customerTrolley.Items)
            {
                var product = products.FirstOrDefault(x => x.Id == item.ProductId);
                if(product != null)
                {
                    item.ProductName = product.Name;
                    item.Description = product.Description;
                    item.UnitPrice = product.Amount;
                }
            }
        }

      

    }
}
