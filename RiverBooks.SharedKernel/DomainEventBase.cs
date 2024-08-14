using MediatR;

namespace RiverBooks.SharedKernel;

public class DomainEventBase : INotification
{
    public DateTime DateCreated { get; protected set; } = DateTime.UtcNow;
}
