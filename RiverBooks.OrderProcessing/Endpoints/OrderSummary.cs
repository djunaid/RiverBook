namespace RiverBooks.OrderProcessing.Endpoints
{
    internal class OrderSummary
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public DateTimeOffset? ShippedDate { get; set; }

        public decimal Total { get; set; }
    }
}