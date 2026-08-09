using E_Commerce.Domain.Contracts;

namespace E_Commerce.API.Extensions
{
    public static class WebApplicationExtensions
    {
        public static async Task<WebApplication> SeedDataBaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var Seeder = scope.ServiceProvider.GetRequiredKeyedService<IDataSeeder>("Catalog");
            var IdentitySeeder = scope.ServiceProvider.GetRequiredKeyedService<IDataSeeder>("Identity");

            await Seeder.SeedAsync();
            await IdentitySeeder.SeedAsync(); 
            return app;
        }
    }
}
