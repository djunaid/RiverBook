using Ardalis.Result;
using Microsoft.Extensions.Logging;
using RiverBooks.OrderProcessing.Interface;
using StackExchange.Redis;
using System.Text.Json;

namespace RiverBooks.OrderProcessing.Infrastructure
{
    internal class RedisOrderAddressCache : IOrderAddressCache
    {
        private readonly IDatabase _db;
        private readonly ILogger<RedisOrderAddressCache> _logger;

        public RedisOrderAddressCache(ILogger<RedisOrderAddressCache> logger)
        {
            _logger = logger;
            var redis = ConnectionMultiplexer.Connect("localhost");
            _db = redis.GetDatabase();
        }

        public async Task<Result<OrderAddress>> GetByIdAsync(Guid addressId)
        {
            string? fetchedJson = await _db.StringGetAsync(addressId.ToString());

            if (fetchedJson is null)
            {
                _logger.LogWarning($"Address {addressId} not found in Redis");
                return Result.NotFound();
            }

            var address = JsonSerializer.Deserialize<OrderAddress>(fetchedJson);

            if (address is null)
            {
                return Result.NotFound();
            }

            _logger.LogInformation($"Address {address.Address} returned from Redis");

            return Result.Success(address);
        }

        public async Task<Result> StoreAsync(OrderAddress orderAddress)
        {
            var key = orderAddress.Id.ToString();
            var address = JsonSerializer.Serialize(orderAddress);

            await _db.StringSetAsync(key, address);

            _logger.LogInformation($"Address stored in REDIS for user {key}");

            return Result.Success();

        }
    }
}