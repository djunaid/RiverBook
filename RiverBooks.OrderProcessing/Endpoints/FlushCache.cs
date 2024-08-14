
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiverBooks.OrderProcessing.Endpoints
{
    internal class FlushCache : FastEndpoints.EndpointWithoutRequest
    {
        private readonly IDatabase _database;
        private readonly ILogger<FlushCache> _logger;

        public FlushCache( ILogger<FlushCache> logger)
        {
            var redis = ConnectionMultiplexer.Connect("localhost");
            _database = redis.GetDatabase();
            _logger = logger;

        }

        public override void Configure()
        {           
            Post("/flushcache");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            await _database.ExecuteAsync("FLUSHDB");

            _logger.LogInformation("Flush Redis DB");
        }
    }
}
