namespace RiverBooks.OrderProcessing.Data
{
    internal interface IOrderRepository
    {
        Task<List<Order>> ListAsync();

        Task AddAsync(Order order);

        Task SaveChangesAsync();
    }
}