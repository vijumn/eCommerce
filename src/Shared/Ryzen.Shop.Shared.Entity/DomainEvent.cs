using MediatR;
namespace Ryzen.Shop.Shared;

public record DomainEvent(Guid Id) : INotification;
