namespace Ryzen.Shop.Trolley.Api.Services
{
    public interface ITrolleyService
    {
        Task<Model.Trolley> GetTrolley(string customerId);
        Task<Model.Trolley> UpdateTrolley(string trolleyId, Model.Trolley customerTrolley);
    }
}
