using Microsoft.EntityFrameworkCore.Infrastructure;
using Ryzen.Shop.Catalog.Application.Data;
using Ryzen.Shop.Catalog.Persistence;

namespace Ryzen.Shop.Catalog.Api.Extensions
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            static void ConfigureSqlOptions(SqlServerDbContextOptionsBuilder sqlOptions)
            {
                sqlOptions.MigrationsAssembly(typeof(CatalogContext).Assembly.FullName);

                // Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 

                sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
            };

            services.AddDbContext<CatalogContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("CatalogDB");

                options.UseSqlServer(connectionString, ConfigureSqlOptions);
            });

            services.AddScoped<ICatalogContext>(sp => sp.GetRequiredService<CatalogContext>());


            return services;
        }
    }
}
