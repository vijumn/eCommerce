namespace Ryzen.Shop.Trolley.Api.Services
{
    public interface ITrolleyService
    {
        Task<CustomerTrolley> GetTrolley(string customerId);
        Task<CustomerTrolley> UpdateTrolley(string trolleyId, CustomerTrolley customerTrolley);
    }
}
