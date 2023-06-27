
namespace Ryzen.Shop.Trolley.API.Repositories;

public class RedisTrolleyRepository : ITrolleyRepository
{
    private readonly ILogger<RedisTrolleyRepository> _logger;
    private readonly ConnectionMultiplexer _redis;
    private readonly IDatabase _database;

    public RedisTrolleyRepository(ILogger<RedisTrolleyRepository> logger, ConnectionMultiplexer redis)
    {
        _logger = logger;
        _redis = redis;
        _database = redis.GetDatabase();
    }

    public async Task<bool> DeleteTrolleyAsync(string id)
    {
        return await _database.KeyDeleteAsync(id);
    }

    public IEnumerable<string> GetUsers()
    {
        var server = GetServer();
        var data = server.Keys();

        return data?.Select(k => k.ToString());
    }

    public async Task<CustomerTrolley> GetTrolleyAsync(string customerId)
    {
        var data = await _database.StringGetAsync(customerId);

        if (data.IsNullOrEmpty)
        {
            return null;
        }

        return JsonSerializer.Deserialize<CustomerTrolley>(data,
            new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            });
    }

    public async Task<CustomerTrolley> UpdateTrolleyAsync(CustomerTrolley trolley)
    {
        var created = await _database.StringSetAsync(trolley.CustomerId, 
            JsonSerializer.Serialize(trolley, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            }));

        if (!created)
        {
            _logger.LogInformation("Problem occur persisting the item.");
            return null;
        }

        _logger.LogInformation("Trolley item persisted successfully.");

        return await GetTrolleyAsync(trolley.CustomerId);
    }

    private IServer GetServer()
    {
        var endpoint = _redis.GetEndPoints();
        return _redis.GetServer(endpoint.First());
    }
}
