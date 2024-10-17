using MediaMarket.Domain.Entities;
using MediaMarket.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MediaMarket.Infrastructure.Extensions
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var sqlServerConnectionString = configuration.GetConnectionString("SqlServer");
            Console.WriteLine(sqlServerConnectionString);
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(sqlServerConnectionString)
                    .EnableSensitiveDataLogging();
            });

            services.AddIdentityCore<User>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            Byte[]? accessSecretKey = Encoding.ASCII.GetBytes(configuration["JwtConfig:AccessSecretKey"]);
            TokenValidationParameters? tokenValidationParams = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = "practical aspnetcore",
                ValidAudience = "https://localhost:5001/",
                IssuerSigningKey = new SymmetricSecurityKey(accessSecretKey),
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwt =>
            {
                jwt.TokenValidationParameters = tokenValidationParams;
            });

            return services;
        }
    }
}
