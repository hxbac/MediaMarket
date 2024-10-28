using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MediaMarket.Infrastructure.Extensions
{
    internal static class MigrationDbContextExtensions
    {
        public static IServiceCollection AddMigration<TContext>(this IServiceCollection services, Func<TContext, IServiceProvider, Task> seeder)
            where TContext : DbContext
        {
            return services.AddHostedService(sp => new MigrationHostedService<TContext>(sp, seeder));
        }

        public static IServiceCollection AddMigration<TContext, TDbSeeder>(this IServiceCollection services)
            where TContext : DbContext
            where TDbSeeder : class, IDbSeeder<TContext>
        {
            services.AddScoped<IDbSeeder<TContext>, TDbSeeder>();
            return services.AddMigration<TContext>((context, sp) => sp.GetRequiredService<IDbSeeder<TContext>>().SeedAsync(context));
        }

        private class MigrationHostedService<TContext>(IServiceProvider serviceProvider, Func<TContext, IServiceProvider, Task> seeder)
            : BackgroundService where TContext : DbContext
        {

            public override Task StartAsync(CancellationToken cancellationToken)
            {
                return serviceProvider.MigrationDbContextAsync(seeder);
            }

            protected override Task ExecuteAsync(CancellationToken stoppingToken)
            {
                return Task.CompletedTask;
            }
        }

        private static async Task MigrationDbContextAsync<TContext>(this IServiceProvider serviceProvider, Func<TContext, IServiceProvider, Task> seeder)
            where TContext : DbContext
        {
            using var scope = serviceProvider.CreateScope();
            var scopeServiceProvider = scope.ServiceProvider;
            var context = scopeServiceProvider.GetService<TContext>();

            try
            {
                var strategy = context.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(() => InvokeSeeder<TContext>(seeder, context, scopeServiceProvider));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private static async Task InvokeSeeder<TContext>(Func<TContext, IServiceProvider, Task> seeder, TContext context, IServiceProvider serviceProvider)
            where TContext : DbContext
        {
            try
            {
                await context.Database.MigrateAsync();
                await seeder(context, serviceProvider);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }

    public interface IDbSeeder<in TContext> where TContext : DbContext
    {
        Task SeedAsync(TContext context);
    }
}
