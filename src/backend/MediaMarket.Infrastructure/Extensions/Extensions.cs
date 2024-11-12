using MediaMarket.Application.Configs;
using MediaMarket.Application.Contracts.Repositories;
using MediaMarket.Application.Contracts.Services;
using MediaMarket.Application.Services;
using MediaMarket.Domain.Entities;
using MediaMarket.Infrastructure.Data;
using MediaMarket.Infrastructure.Payment;
using MediaMarket.Infrastructure.Repositories;
using MediaMarket.Infrastructure.Seeders;
using MediaMarket.Infrastructure.Storage;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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

            //services.AddHostedService<InitialStorageFolder>();

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
                        var errorMessage = context.Exception.Message;

                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.ContentType = "application/json";
                        var response = new
                        {
                            error = true,
                            message = "Invalid token",
                            detail = errorMessage
                        };
                        return context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
                    }
                };
            });

            services.AddCors(options =>
            {
                options.AddPolicy(name: "frontend",
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:8080")
                            .AllowAnyMethod()
                            .AllowCredentials()
                            .AllowAnyHeader();
                    });
            });

            services.AddScoped<IProductDetailRepository, ProductDetailRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IPreviewRepository, PreviewRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IVideoSolutionRepository, VideoSolutionRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddScoped<ITagService, TagService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IOrderService, OrderService>();

            services.AddSingleton<IFileService, LocalStorageFileService>();
            services.AddSingleton<IPaymentService, StripePaymentService>();

            return services;
        }

        public static IServiceCollection AddMigrations(this IServiceCollection services)
        {
            //services.AddMigration<ApplicationDbContext, TagSeeder>();
            services.AddMigration<ApplicationDbContext, CategorySeeder>();
            //services.AddMigration<ApplicationDbContext, ProductSeeder>();

            return services;
        }
    }

    public class InitialStorageFolder(ILogger<InitialStorageFolder> _logger) : BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogDebug("Start initial folder");

            var root = Directory.GetCurrentDirectory();
            string publicFolderPath = Path.Combine(root, "wwwroot/storage");
            string targetFolderPath = Path.Combine(root, "storage/uploads/public");

            if (File.Exists(publicFolderPath))
            {
                var fileInfo = new FileInfo(publicFolderPath);
                if (fileInfo.Attributes.HasFlag(FileAttributes.ReparsePoint))
                {
                    _logger.LogDebug("Symlink đã tồn tại.");
                }
                else
                {
                    _logger.LogError("Tệp đã tồn tại nhưng không phải là symlink.");
                }
            }
            else
            {
                try
                {
                    Directory.CreateSymbolicLink(publicFolderPath, targetFolderPath);
                    _logger.LogDebug($"Đã tạo symlink từ {publicFolderPath} tới {targetFolderPath}.");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Lỗi khi tạo symlink: {ex.Message}");
                }
            }

            return Task.CompletedTask;
        }
    }
}
