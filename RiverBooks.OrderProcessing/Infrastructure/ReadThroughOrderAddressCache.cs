using Ardalis.Result;
using MediatR;
using Microsoft.Extensions.Logging;
using RiverBooks.OrderProcessing.Domain;
using RiverBooks.OrderProcessing.Interface;
using RiverBooks.Users.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiverBooks.OrderProcessing.Infrastructure
{
    internal class ReadThroughOrderAddressCache : IOrderAddressCache
    {
        private readonly RedisOrderAddressCache _redisCache;
        private readonly IMediator _mediator;
        private readonly ILogger<ReadThroughOrderAddressCache> _logger;

        public ReadThroughOrderAddressCache(RedisOrderAddressCache redisCache, IMediator mediator, ILogger<ReadThroughOrderAddressCache> logger)
        {
            _redisCache = redisCache;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task<Result<OrderAddress>> GetByIdAsync(Guid addressId)
        {
            var result = await _redisCache.GetByIdAsync(addressId);
            if (result.IsSuccess) { return result; }

            if (result.Status == ResultStatus.NotFound)
            {
                _logger.LogInformation($"Address {addressId} not found, fetching from source");

                var query = new UserAddressDetailsByIdQuery(addressId);

                var queryResult = await _mediator.Send(query);

                if (queryResult.IsSuccess)
                {
                    var addressDto = queryResult.Value;

                    var address = new Address(addressDto.Street1, addressDto.Street2, addressDto.City, addressDto.State, addressDto.PostalCode, addressDto.Country);

                    var orderAddress = new OrderAddress(addressDto.AddressId, address);
                    await StoreAsync(orderAddress);
                    return orderAddress;
                }

            }

            return Result.NotFound();
        }

        public Task<Result> StoreAsync(OrderAddress orderAddress)
        {
            return _redisCache.StoreAsync(orderAddress);
        }
    }
}
