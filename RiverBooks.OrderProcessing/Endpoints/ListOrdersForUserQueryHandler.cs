using Ardalis.Result;
using MediatR;
using RiverBooks.OrderProcessing.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiverBooks.OrderProcessing.Endpoints
{
    internal class ListOrdersForUserQueryHandler : IRequestHandler<ListOrdersForUserQuery, Result<List<OrderSummary>>>
    {
        private readonly IOrderRepository _orderRepository;

        public ListOrdersForUserQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Result<List<OrderSummary>>> Handle(ListOrdersForUserQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.ListAsync();

            var summaries = orders.Select(x => new OrderSummary
            {
                OrderId = x.Id,
                UserId = x.UserID,
                CreatedDate = x.DateCreated,
                Total = x.OrderItems.Sum(oi => oi.UnitPrice)
            }).ToList();

            return summaries;
        }
    }
}
