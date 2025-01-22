using MediaMarket.Application.Events;
using MediaMarket.Application.Listeners;
using Microsoft.Extensions.DependencyInjection;

namespace MediaMarket.Infrastructure.Event
{
    public static class Extensions
    {
        public static IServiceCollection AddEvents(this IServiceCollection services)
        {
            services.AddSingleton<EventManager>();

            using var serviceProvider = services.BuildServiceProvider();
            var eventManager = serviceProvider.GetRequiredService<EventManager>();
            eventManager.AddListener<CreateUserEvent>(typeof(TestListener));

            return services;
        }
    }
}
