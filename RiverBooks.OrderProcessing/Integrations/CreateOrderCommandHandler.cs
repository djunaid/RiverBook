using Ardalis.Result;
using MediatR;
using Serilog;
using RiverBooks.OrderProcessing.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RiverBooks.OrderProcessing.Domain;
using RiverBooks.OrderProcessing.Interface;

namespace RiverBooks.OrderProcessing.Integrations
{
    internal class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Result<OrderDetailResponse>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger _logger;
        private readonly IOrderAddressCache _orderAddressCache;

        public CreateOrderCommandHandler(IOrderRepository orderRepository, ILogger logger, IOrderAddressCache orderAddressCache)
        {
            _orderRepository = orderRepository;
            _logger = logger;
            _orderAddressCache = orderAddressCache;
        }

        public async Task<Result<OrderDetailResponse>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var items = request.OrderItems.Select(item => new OrderItem
            (
                 item.BookId,
                 item.Description,
                 item.Quantity,
                 item.UnitPrice
            ));

            //  var billingAddress = new Address("ABC Street", "", "Kent", "CA", "12345", "USA");
            //  var shippingAddress = billingAddress;

            var shippingAddress = await _orderAddressCache.GetByIdAsync(request.ShippingAddressId);
            var billingAddress = await _orderAddressCache.GetByIdAsync(request.BillingAddressId);

            var newOrder = Order.Factory.Create(request.UserId, shippingAddress.Value.Address, billingAddress.Value.Address, items);

            await _orderRepository.AddAsync(newOrder);
            await _orderRepository.SaveChangesAsync();

            _logger.Information($"New order created: {newOrder.Id}");

            return new OrderDetailResponse(newOrder.Id);


        }
    }
}
