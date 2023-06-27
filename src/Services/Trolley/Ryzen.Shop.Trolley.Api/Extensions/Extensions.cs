using System;

namespace Ryzen.Shop.Trolley.Api.Extensions;
public static class Extensions
{
    //public static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
    //{
    //    services.AddHealthChecks()
    //        .AddRedis(_ => configuration.GetRequiredConnectionString("redis"), "redis", tags: new[] { "ready", "liveness" });

    //    return services;
    //}

    public static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddSingleton(sp =>
        {
            var redisConfig = ConfigurationOptions.Parse(configuration.GetConnectionString("redis"), true);

            return ConnectionMultiplexer.Connect(redisConfig);
        });
    }
}
