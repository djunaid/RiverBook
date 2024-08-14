using MediatR;
using Microsoft.Extensions.Logging;
using RiverBooks.OrderProcessing.Domain;
using RiverBooks.OrderProcessing.Infrastructure;
using RiverBooks.OrderProcessing.Interface;
using RiverBooks.Users.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiverBooks.OrderProcessing.Integrations
{
    internal class AddressCacheUpdatingNewUserAddressHandler : INotificationHandler<NewUserAddressIntegrationEvent>
    {
        private readonly ILogger<AddressCacheUpdatingNewUserAddressHandler> logger;
        private readonly IOrderAddressCache orderAddressCache;

        public AddressCacheUpdatingNewUserAddressHandler(
            ILogger<AddressCacheUpdatingNewUserAddressHandler> logger,
            IOrderAddressCache orderAddressCache) {
            this.logger = logger;
            this.orderAddressCache = orderAddressCache;
        }
        public async Task Handle(NewUserAddressIntegrationEvent notification, CancellationToken cancellationToken)
        {
            var orderAddress = new OrderAddress(notification.userAddressDetails.AddressId,
                                    new Address(notification.userAddressDetails.Street1, notification.userAddressDetails.Street2, notification.userAddressDetails.City, notification.userAddressDetails.PostalCode
                                    , notification.userAddressDetails.State, notification.userAddressDetails.Country));

            await orderAddressCache.StoreAsync(orderAddress);

            logger.LogInformation($"[OrderProcessing] Address update in cache : {orderAddress}");
        }
    }
}
