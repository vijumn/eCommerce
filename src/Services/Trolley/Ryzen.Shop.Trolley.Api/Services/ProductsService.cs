using Ryzen.Shop.Shared;

namespace Ryzen.Shop.Trolley.Api.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<PromotionsService> _logger;
        private readonly HttpClient _catalogClient;

        public ProductsService(IHttpClientFactory httpClientFactory, ILogger<PromotionsService> logger)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _catalogClient = _httpClientFactory.CreateClient("CatalogApi");
        }

        public async Task<List<Product>> GetProducts(List<int> productIds)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            string idString = string.Join("&ids=", productIds);

            try
            {
                var response = await _catalogClient.GetAsync($"Products/Search?ids={idString}");
                response.EnsureSuccessStatusCode();
                string jsonString = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Product>>(jsonString, options);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error while getting products");
                throw;
            }

        }

    }


    public class Product
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
    }

}
