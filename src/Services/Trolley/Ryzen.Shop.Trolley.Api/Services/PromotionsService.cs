using Ryzen.Shop.Shared;

namespace Ryzen.Shop.Trolley.Api.Services
{
    public class PromotionsService : IPromotionsService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<PromotionsService> _logger;
        private readonly HttpClient _catalogClient;

        public PromotionsService(IHttpClientFactory httpClientFactory, ILogger<PromotionsService> logger)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _catalogClient = _httpClientFactory.CreateClient("CatalogApi");
        }

        public async Task<List<Promotion>> GetPromotions(List<int> productIds)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            string idString = string.Join("&ids=", productIds);

            try
            {
                var response = await _catalogClient.GetAsync($"Promotions/Search?ids={idString}");
                response.EnsureSuccessStatusCode();
                string jsonString = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Promotion>>(jsonString,options);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error while getting products");
                throw;
            }

        }

    }
  

    public class Promotion
    {
        public int? ProductId { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public int? PromotionId { get; set; }
        public PromotionType Type { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public decimal? MinimumSpend { get; set; }
        public bool? GetOneFree { get; set; }
        public decimal? SecondOneDiscountPercentage { get; set; }
    }

    public class PromotionType : Enumeration
    {

        public static PromotionType ItemDiscount = new(1, nameof(ItemDiscount));
        public static PromotionType MinimumSpend = new(2, nameof(MinimumSpend));
        public static PromotionType GetOneFree = new(3, nameof(GetOneFree));
        public static PromotionType SecondOneDiscountPercentage = new PromotionType(4, nameof(SecondOneDiscountPercentage));
        public static PromotionType TrollyDiscount = new PromotionType(5, nameof(TrollyDiscount));

        public PromotionType(int id, string name)
      : base(id, name)
        {
        }

        public static explicit operator int(PromotionType v)
        {
            return v.Id;
        }
    }

}
