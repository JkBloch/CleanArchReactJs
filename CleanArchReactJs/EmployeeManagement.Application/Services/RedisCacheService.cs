using EmployeeManagement.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
using System.Text.Json;
namespace EmployeeManagement.Application.Services
{
    public class RedisCacheService : IRedisCacheService
    {
        private readonly IDatabase _database;

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public RedisCacheService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            var value = await _database.StringGetAsync(key);

            if (value.IsNullOrEmpty)
                return default;

            return JsonSerializer.Deserialize<T>(
                value.ToString(),
                JsonOptions);
        }

        public async Task SetAsync<T>(
            string key,
            T value,
            TimeSpan expiration)
        {
            var json = JsonSerializer.Serialize(
                value,
                JsonOptions);

            await _database.StringSetAsync(
                key,
                json,
                expiration);
        }

        public async Task RemoveAsync(string key)
        {
            await _database.KeyDeleteAsync(key);
        }

        public async Task RemoveByPrefixAsync(string prefix)
        {
            var endpoints =
                _database.Multiplexer.GetEndPoints();

            foreach (var endpoint in endpoints)
            {
                var server =
                    _database.Multiplexer
                        .GetServer(endpoint);

                var keys = server.Keys(
                    pattern: $"{prefix}*");

                foreach (var key in keys)
                {
                    await _database.KeyDeleteAsync(key);
                }
            }
        }

        public async Task RemoveByPatternAsync(string pattern)
        {
            foreach (var endpoint in _database.Multiplexer.GetEndPoints())
            {
                var server = _database.Multiplexer.GetServer(endpoint);

                foreach (var key in server.Keys(
                    _database.Database,
                    pattern: pattern))
                {
                    await _database.KeyDeleteAsync(key);
                }
            }
        }

    }
}
