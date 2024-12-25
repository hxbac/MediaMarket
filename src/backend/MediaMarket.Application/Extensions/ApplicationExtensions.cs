using MediaMarket.Application.Mappings;
using Microsoft.Extensions.DependencyInjection;

namespace MediaMarket.Application.Extensions
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddAutoMappers(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(TagMappingProfile));
            services.AddAutoMapper(typeof(CategoryMappingProfile));
            services.AddAutoMapper(typeof(ProductMappingProfile));
            services.AddAutoMapper(typeof(UserMappingProfile));
            services.AddAutoMapper(typeof(WithdrawalMappingProfile));
            services.AddAutoMapper(typeof(EventDiscountMappingProfile));

            return services;
        }
    }
}
