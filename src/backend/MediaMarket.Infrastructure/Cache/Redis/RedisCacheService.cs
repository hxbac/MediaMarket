using MediaMarket.Application.Contracts.Infrastructures.Cache;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System.Text.Json;

namespace MediaMarket.Infrastructure.Cache.Redis
{
    public class RedisCacheService : ICacheService
    {
        private readonly RedisCacheOptions _redisCacheOptions;

        private readonly Lazy<ConnectionMultiplexer> _lazyConnection;
        public ConnectionMultiplexer ConnectionMultiplexer => _lazyConnection.Value;

        private readonly SemaphoreSlim _connectionLock = new SemaphoreSlim(1, 1);

        public RedisCacheService(IOptions<RedisCacheOptions> redisCacheOptions)
        {
            _redisCacheOptions = redisCacheOptions.Value;
            _lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
                ConnectionMultiplexer.Connect(redisCacheOptions.Value.GetConnectionString()));
        }

        public IDatabase Database
        {
            get
            {
                _connectionLock.Wait();

                try
                {
                    return ConnectionMultiplexer.GetDatabase();
                }
                finally
                {
                    _connectionLock.Release();
                }
            }
        }

        public async Task SetAsync<T>(string key, T obj)
        {
            await SetAsync(key, obj, TimeSpan.FromSeconds(_redisCacheOptions.RedisDefaultSlidingExpirationInSecond));
        }

        public async Task SetAsync<T>(string key, T obj, TimeSpan expiration)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException("Key cannot be null, empty, or only whitespace.");
            }

            var keyWithPrefix = $"{_redisCacheOptions.Prefix}:{key}";
            await Database.StringSetAsync(keyWithPrefix, JsonSerializer.SerializeToUtf8Bytes(obj), expiration);
        }

        public Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> func)
        {
            return GetOrSetAsync(key, func,
                TimeSpan.FromSeconds(_redisCacheOptions.RedisDefaultSlidingExpirationInSecond));
        }

        public async Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> func, TimeSpan expiration)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException("Key cannot be null, empty, or only whitespace.");
            }

            var keyWithPrefix = $"{_redisCacheOptions.Prefix}:{key}";
            var valueAsString = await Database.StringGetAsync(keyWithPrefix);
            if (!string.IsNullOrEmpty(valueAsString))
            {
                return GetByteToObject<T>(valueAsString);
            }

            var value = await func();
            if (value != null)
            {
                await SetAsync(key, value, expiration);
            }

            return value;
        }

        public async Task<T?> GetValueAsync<T>(string key)
        {
            var keyWithPrefix = $"{_redisCacheOptions.Prefix}:{key}";

            var valueAsString = await Database.StringGetAsync(keyWithPrefix);
            if (!string.IsNullOrEmpty(valueAsString))
            {
                return GetByteToObject<T>(valueAsString);
            }

            return default;
        }

        public async Task<IEnumerable<T>> GetValuesAsync<T>(string key)
        {
            var keyWithPrefix = $"{_redisCacheOptions.Prefix}:{key}";
            var items = await Database.HashGetAllAsync(keyWithPrefix);
            return items.Select(x => GetByteToObject<T>(x.Value));
        }

        private static T GetByteToObject<T>(RedisValue value)
        {
            var readOnlySpan = new ReadOnlySpan<byte>(value);
            return JsonSerializer.Deserialize<T>(readOnlySpan);
        }

        public async Task RemoveAsync(string key)
        {
            var keyWithPrefix = $"{_redisCacheOptions.Prefix}:{key}";
            await Database.KeyDeleteAsync(keyWithPrefix);
        }
    }
}
