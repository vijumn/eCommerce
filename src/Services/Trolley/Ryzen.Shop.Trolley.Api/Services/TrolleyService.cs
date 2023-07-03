using Ryzen.Shop.Trolley.Api.Business;
using Ryzen.Shop.Trolley.Api.Exceptions;
using Ryzen.Shop.Trolley.Api.Model;

namespace Ryzen.Shop.Trolley.Api.Services
{
    public class TrolleyService : ITrolleyService
    {
        private readonly IPromotionsService _promotionsService;
        private readonly IDiscountEngine _discountEngine;
        private readonly IProductsService _productsService;
        private readonly ITrolleyRepository _trolleyRepository;
        public TrolleyService(IPromotionsService promotionsService, IDiscountEngine discountEngine, IProductsService productsService, ITrolleyRepository trolleyRepository)
        {
            _promotionsService = promotionsService;
            _discountEngine = discountEngine;
            _productsService = productsService;
            _trolleyRepository = trolleyRepository;
        }

        public async Task<Model.Trolley> GetTrolley(string trolleyId)
        {
            return await _trolleyRepository.GetTrolleyAsync(trolleyId);
        }
        public async Task<Model.Trolley> UpdateTrolley(string trolleyId, Model.Trolley customerTrolley)
        {
            var ids = customerTrolley.Items.Select(x => x.ProductId).ToList();
            var promotions =await _promotionsService.GetPromotions(ids);
            var products = await _productsService.GetProducts(ids);
            EnrichTrolley(customerTrolley, products);
            var trolley = await _discountEngine.ApplyDiscount(promotions, customerTrolley);

            await _trolleyRepository.UpdateTrolleyAsync(customerTrolley);

            return customerTrolley;

        }
        private void EnrichTrolley(Model.Trolley customerTrolley, List<Product> products)
        {
              foreach(var item in customerTrolley.Items)
            {
                var product = products.FirstOrDefault(x => x.Id == item.ProductId);

                if(product == null)
                {
                   throw new ProductNotFoundException(item.ProductId);
                }
                item.ProductName = product.Name;
                item.Description = product.Description;
                item.UnitPrice = product.Amount;
            }
        }

      

    }
}
