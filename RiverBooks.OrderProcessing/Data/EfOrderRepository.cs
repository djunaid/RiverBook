
using Microsoft.EntityFrameworkCore;

namespace RiverBooks.OrderProcessing.Data
{
    internal class EfOrderRepository : IOrderRepository
    {
        private readonly OrderDBContext _dbContext;

        public EfOrderRepository(OrderDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Order order)
        {
            await _dbContext.Orders.AddAsync(order);
        }

        public async Task<List<Order>> ListAsync()
        {
            return await _dbContext.Orders.Include(x=> x.OrderItems).ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}