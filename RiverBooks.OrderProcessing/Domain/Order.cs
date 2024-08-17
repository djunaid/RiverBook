using RiverBooks.SharedKernel;
using System.ComponentModel.DataAnnotations.Schema;

namespace RiverBooks.OrderProcessing.Domain
{
    internal class Order : IHaveDomainEvents
    {
        public Guid Id { get; private set; } = Guid.NewGuid();

        public Guid UserID { get; private set; }

        public Address ShippingAddress { get; private set; } = default!;

        public Address BillingAddress { get; private set; } = default!;

        private List<OrderItem> _orderItems = new();

        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

        public DateTimeOffset DateCreated { get; private set; } = DateTimeOffset.UtcNow;

        private List<DomainEventBase> _domainEvents = new();

        [NotMapped]
        public IEnumerable<DomainEventBase> DomainEvents => _domainEvents.AsReadOnly();

        protected void RegisterDomainEvent(DomainEventBase domainEvent) => _domainEvents.Add(domainEvent);

        private void AddOrderItem(OrderItem item) => _orderItems.Add(item);

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

        internal class Factory
        {
            public static Order Create(Guid UserId, Address ShippingAddress, Address BillingAddress, IEnumerable<OrderItem> OrderItems)
            {
                var order = new Order();
                order.UserID = UserId;
                order.ShippingAddress = ShippingAddress;
                order.BillingAddress = BillingAddress;

                foreach (var item in OrderItems)
                {
                    order.AddOrderItem(item);
                }

                var orderCreatedEvent = new OrderCreatedEvent(order);

                order.RegisterDomainEvent(orderCreatedEvent);

                return order;
            }
        }

    }
}
