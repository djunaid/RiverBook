namespace RiverBooks.OrderProcessing.Domain
{
    internal class Order
    {
        public Guid Id { get; private set; } = Guid.NewGuid();

        public Guid UserID { get; private set; }

        public Address ShippingAddress { get; private set; } = default!;

        public Address BillingAddress { get; private set; } = default!;

        private List<OrderItem> _orderItems = new();

        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

        public DateTimeOffset DateCreated { get; private set; } = DateTimeOffset.UtcNow;

        private void AddOrderItem(OrderItem item) => _orderItems.Add(item);

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

                return order;
            }
        }

    }
}
