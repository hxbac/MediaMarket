using MediaMarket.Application.Contracts.Infrastructures.Cache;
using MediaMarket.Infrastructure.Cache.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace MediaMarket.Infrastructure.Cache
{
    public static class Extensions
    {
        public static IServiceCollection AddRedisCache(this IServiceCollection services, IConfiguration configuration)
        {
            var redisOptions = new RedisCacheOptions();
            var redisSection = configuration.GetSection("Redis");

            redisSection.Bind(redisOptions);
            services.Configure<RedisCacheOptions>(redisSection);

            services.AddStackExchangeRedisCache(options =>
            {
                options.InstanceName = configuration[redisOptions.Prefix];
                options.ConfigurationOptions = GetRedisConfigurationOptions(redisOptions);
            });

            services.AddSingleton<ICacheService, RedisCacheService>();

            return services;
        }

        private static ConfigurationOptions GetRedisConfigurationOptions(RedisCacheOptions redisOptions)
        {
            var configurationOptions = new ConfigurationOptions
            {
                ConnectTimeout = redisOptions.ConnectTimeout,
                SyncTimeout = redisOptions.SyncTimeout,
                ConnectRetry = redisOptions.ConnectRetry,
                AbortOnConnectFail = redisOptions.AbortOnConnectFail,
                ReconnectRetryPolicy = new ExponentialRetry(redisOptions.DeltaBackoffMiliseconds),
                KeepAlive = 5,
                Ssl = redisOptions.Ssl
            };

            if (!string.IsNullOrWhiteSpace(redisOptions.Password))
            {
                configurationOptions.Password = redisOptions.Password;
            }

            var endpoints = redisOptions.Url.Split(',');
            foreach (var endpoint in endpoints)
            {
                configurationOptions.EndPoints.Add(endpoint);
            }

            return configurationOptions;
        }
    }
}
