using MediaMarket.Application.Configs;
using MediaMarket.Application.Contracts.Repositories;
using MediaMarket.Application.Contracts.Services;
using MediaMarket.Application.Services;
using MediaMarket.Domain.Entities;
using MediaMarket.Infrastructure.Data;
using MediaMarket.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

namespace MediaMarket.Infrastructure.Extensions
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(
                    "storage/logs/log-.log",
                    rollingInterval: RollingInterval.Day,
                    shared: true,
                    flushToDiskInterval: TimeSpan.FromSeconds(1)
                )
                .CreateLogger();
            services.AddSerilog();

            var sqlServerConnectionString = configuration.GetConnectionString("SqlServer");
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(sqlServerConnectionString)
                    .EnableSensitiveDataLogging();
            });

            services.Configure<JwtConfig>(configuration.GetSection("JwtConfig"));
            var jwtConfig = configuration.GetSection("JwtConfig").Get<JwtConfig>();

            services.AddIdentityCore<User>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            Byte[]? accessSecretKey = Encoding.ASCII.GetBytes(jwtConfig.SecretKey);
            var tokenValidationParams = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtConfig.Issuer,
                ValidAudience = jwtConfig.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(accessSecretKey),
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwt =>
            {
                jwt.TokenValidationParameters = tokenValidationParams;

                jwt.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.ContentType = "application/json";
                        return context.Response.WriteAsync("{\"error\": \"Invalid token\"}");
                    }
                };
            });

            services.AddCors(options =>
            {
                options.AddPolicy(name: "frontend",
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:8080");
                    });
            });

            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IPreviewRepository, PreviewRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddScoped<ITagService, TagService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();

            return services;
        }

        public static IServiceCollection AddMigrations(this IServiceCollection services)
        {
            //services.AddMigration<ApplicationDbContext, TagSeeder>();
            //services.AddMigration<ApplicationDbContext, CategorySeeder>();
            //services.AddMigration<ApplicationDbContext, ProductSeeder>();

            return services;
        }
    }
}
