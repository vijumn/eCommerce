using Ryzen.Shop.Trolley.Api.Model;

namespace Ryzen.Shop.Trolley.Api.Model;

public interface ITrolleyRepository
{
    Task<CustomerTrolley> GetTrolleyAsync(string customerId);
    Task<CustomerTrolley> UpdateTrolleyAsync(CustomerTrolley basket);
    Task<bool> DeleteTrolleyAsync(string id);

    IEnumerable<string> GetUsers();
}

