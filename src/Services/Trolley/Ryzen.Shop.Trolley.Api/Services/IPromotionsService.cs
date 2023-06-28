namespace Ryzen.Shop.Trolley.Api.Services
{
    public interface IPromotionsService
    {
        Task<List<Promotion>> GetPromotions(List<int> Ids);
    }
}