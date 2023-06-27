using Microsoft.Extensions.Logging;
using Ryzen.Shop.Catalog.Persistence;

namespace Ryzen.Shop.Catalog.Api.Extensions
{
    public static class MigrationExtensions
    {
        public static async void ApplyMigrations(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<CatalogContext>();
            dbContext.Database.Migrate();

            await new CatalogContextSeed().SeedAsync(dbContext);
        }
    }
}
