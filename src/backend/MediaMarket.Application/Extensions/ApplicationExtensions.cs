using MediaMarket.Application.Mappings;
using Microsoft.Extensions.DependencyInjection;

namespace MediaMarket.Application.Extensions
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddAutoMappers(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(TagMappingProfile));
            return services;
        }
    }
}
