using MassTransit;
using Microsoft.Extensions.Logging;
using Ryzen.Shop.Events;

namespace Ryzen.Shop.Catalog.Application.EventHandlers;

public class TrolleyDeletedEventConsumer : IConsumer<TrolleyDeletedEvent>
{
    private readonly ILogger<TrolleyDeletedEventConsumer> _logger;

    public TrolleyDeletedEventConsumer(ILogger<TrolleyDeletedEventConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<TrolleyDeletedEvent> context)
    {
        _logger.LogInformation($"Trolley Deleted for CustomerId: {context.Message.CustomerId}");

        _logger.LogInformation(context.Message.ToString());
        return Task.CompletedTask;
    }
}
