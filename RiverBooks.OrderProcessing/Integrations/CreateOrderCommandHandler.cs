using Ardalis.Result;
using MediatR;
using Microsoft.Extensions.Logging;
using RiverBooks.OrderProcessing.Contracts;
using RiverBooks.OrderProcessing.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiverBooks.OrderProcessing.Integrations
{
    internal class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Result<OrderDetailResponse>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger _logger;

        public CreateOrderCommandHandler(IOrderRepository orderRepository, ILogger logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
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

            var billingAddress = new Address("ABC Street", "", "Kent", "CA", "12345", "USA");
            var shippingAddress = billingAddress;

            var newOrder = Order.Factory.Create(request.UserId, shippingAddress, billingAddress, items);

            await _orderRepository.AddAsync(newOrder);
            await _orderRepository.SaveChangesAsync();

            _logger.LogInformation($"New order created: {newOrder.Id}");

            return new OrderDetailResponse(newOrder.Id);


        }
    }
}
