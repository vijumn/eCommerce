using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Ryzen.Shop.Catalog.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblyContaining<ApplicationAssemblyReference>();

        });

        services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();
            x.SetInMemorySagaRepositoryProvider();

            x.AddConsumers(ApplicationAssemblyReference.Assembly);
            x.AddSagaStateMachines(ApplicationAssemblyReference.Assembly);
            x.AddSagas(ApplicationAssemblyReference.Assembly);
            x.AddActivities(ApplicationAssemblyReference.Assembly);

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("eshop-mq", "/", h => {
                    h.Username("guest");
                    h.Password("guest");
                });
                cfg.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}
