using RiverBooks.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiverBooks.OrderProcessing.Domain
{
    internal sealed class OrderCreatedEvent : DomainEventBase
    {
       
        public Order NewOrder { get; }

        public OrderCreatedEvent(Order newOrder)
        {
            NewOrder = newOrder;
        }
    }
}
