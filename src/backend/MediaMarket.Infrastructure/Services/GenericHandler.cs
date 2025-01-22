using MediaMarket.Application.Bases;
using MediaMarket.Application.Contracts.Common;
using Microsoft.Extensions.DependencyInjection;

namespace MediaMarket.Infrastructure.Services
{
    public class GenericHandler
    {
        private readonly IServiceProvider _serviceProvider;

        public GenericHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task InvokeAsync<T>(EventBase @event) where T : ITaskConsumer
        {
            var type = typeof(T);
            var constructor = type.GetConstructors()
                .OrderByDescending(c => c.GetParameters().Length)
                .FirstOrDefault();

            if (constructor == null)
            {
                throw new Exception("No constructor found");
            }

            var constructorParams = constructor.GetParameters();
            if (constructorParams.Length == 0)
            {
                var instance = (T)constructor.Invoke(null);
                await instance.HandleAsync(@event);
            }
            else
            {
                using var scopeServiceProvider = _serviceProvider.CreateScope();
                var parameters = constructor.GetParameters()
                    .Select(p => scopeServiceProvider.ServiceProvider.GetRequiredService(p.ParameterType))
                    .ToArray();
                var instance = (T)constructor.Invoke(parameters);
                await instance.HandleAsync(@event);
            }
        }
    }
}
