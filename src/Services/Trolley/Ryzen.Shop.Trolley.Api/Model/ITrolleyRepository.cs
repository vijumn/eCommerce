using Ryzen.Shop.Trolley.Api.Model;

namespace Ryzen.Shop.Trolley.Api.Model;

public interface ITrolleyRepository
{
    Task<Trolley> GetTrolleyAsync(string customerId);
    Task<Trolley> UpdateTrolleyAsync(Trolley basket);
    Task<bool> DeleteTrolleyAsync(string id);

    IEnumerable<string> GetUsers();
}

