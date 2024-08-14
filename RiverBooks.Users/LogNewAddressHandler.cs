using MediatR;
using Microsoft.Extensions.Logging;
using RiverBooks.Users.Domain;

namespace RiverBooks.Users;

internal class LogNewAddressHandler : INotificationHandler<AddressAddedEvent>
{
    private readonly ILogger<LogNewAddressHandler> _logger;

    public LogNewAddressHandler(ILogger<LogNewAddressHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(AddressAddedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"[DE Handler] Address added for {notification.NewAddress.UserId} : {notification.NewAddress.StreetAddress} ");

        return Task.CompletedTask;
    }
}
