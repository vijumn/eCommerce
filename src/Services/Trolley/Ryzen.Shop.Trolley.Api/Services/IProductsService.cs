namespace Ryzen.Shop.Trolley.Api.Services
{
    public interface IProductsService
    {
        Task<List<Product>> GetProducts(List<int> productIds);
    }
}