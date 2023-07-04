using System;
using System.Reflection;
using Ryzen.Shop.Infrastructure.MessageBroker;
using MassTransit;

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
    public static IServiceCollection AddMessaging(this IServiceCollection services)
    {
        services.AddMassTransit(x =>
        {
            x.AddDelayedMessageScheduler();

            x.SetKebabCaseEndpointNameFormatter();

            // By default, sagas are in-memory, but should be changed to a durable
            // saga repository.
            x.SetInMemorySagaRepositoryProvider();

            var entryAssembly = Assembly.GetEntryAssembly();

            x.AddConsumers(entryAssembly);
            x.AddSagaStateMachines(entryAssembly);
            x.AddSagas(entryAssembly);
            x.AddActivities(entryAssembly);

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("eshop-mq", "/", h => {
                    h.Username("guest");
                    h.Password("guest");
                });

                cfg.UseDelayedMessageScheduler();

                cfg.ConfigureEndpoints(context);
            });
        });
        services.AddTransient<IEventBus, EventBus>();
        return services;
    }
}
