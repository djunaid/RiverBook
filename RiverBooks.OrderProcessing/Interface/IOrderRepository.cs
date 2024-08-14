using RiverBooks.OrderProcessing.Domain;

namespace RiverBooks.OrderProcessing.Interface
{
    internal interface IOrderRepository
    {
        Task<List<Order>> ListAsync();

        Task AddAsync(Order order);

        Task SaveChangesAsync();
    }
}