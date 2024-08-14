using MediatR;
using Microsoft.Extensions.Logging;
using RiverBooks.Users.Contracts;
using RiverBooks.Users.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiverBooks.Users.Integrations
{
    internal class UserAddressIntegrationEventDispatcherHandler : INotificationHandler<AddressAddedEvent>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UserAddressIntegrationEventDispatcherHandler> _logger;

        public UserAddressIntegrationEventDispatcherHandler(IMediator mediator, ILogger<UserAddressIntegrationEventDispatcherHandler> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public async Task Handle(AddressAddedEvent notification, CancellationToken ct)
        {
            Guid userId = notification.NewAddress.UserId;

            var addressDetail = new UserAddressDetails(userId,
                                notification.NewAddress.Id,
                                notification.NewAddress.StreetAddress.Street1,
                                notification.NewAddress.StreetAddress.Street2,
                                notification.NewAddress.StreetAddress.City,
                                notification.NewAddress.StreetAddress.PostalCode,
                                notification.NewAddress.StreetAddress.State,
                                notification.NewAddress.StreetAddress.Country);

            await _mediator.Publish(new NewUserAddressIntegrationEvent(addressDetail));

            _logger.LogInformation($"[DE Handler1]New address event sent for user {userId} address: {notification.NewAddress.StreetAddress}");


           
        }
    }
}
