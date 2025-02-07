using MediaMarket.Application.Events;
using MediaMarket.Application.Listeners;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MediaMarket.Infrastructure.Event
{
    public static class Extensions
    {
        public static IServiceCollection AddEvents(this IServiceCollection services)
        {
            services.AddSingleton<EventManager>();
            services.AddHostedService<InitialEventsBackgroundService>();

            return services;
        }
    }

    public class InitialEventsBackgroundService(
        ILogger<InitialEventsBackgroundService> logger,
        IServiceProvider serviceProvider) : IHostedService
    {
        private readonly ILogger<InitialEventsBackgroundService> _logger = logger;
        private readonly IServiceProvider _serviceProvider = serviceProvider;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var eventManager = _serviceProvider.GetRequiredService<EventManager>();
            eventManager.AddListener<CreateUserEvent>(typeof(TestListener));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
