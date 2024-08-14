using MediatR;

namespace RiverBooks.Users.Contracts
{
    public abstract record IntegrationEventBase : INotification
    {
        DateTimeOffset DateTimeOffset { get; set; } = DateTimeOffset.UtcNow;
    }
}
