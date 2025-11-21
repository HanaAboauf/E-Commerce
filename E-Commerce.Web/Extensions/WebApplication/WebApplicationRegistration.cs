using E_Commerce.Domain.Contracts;
using E_Commerce.Persistence.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;

namespace E_Commerce.Web.Extensions.WebApplication
{
    public static class WebApplicationRegistration
    {
        //public static WebApplication MigrateDatabase(this WebApplication app)
        //{
        //    using var scope = app.Services.CreateScope();
        //    var dbService = scope.ServiceProvider.GetRequiredService<StoreDbContext>();
        //    if (dbService.Database.GetPendingMigrations().Any()) dbService.Database.Migrate();
        //    return app;
        //}
        //public static WebApplication SeedDatabase(this WebApplication app)
        //{
        //    using var scope = app.Services.CreateScope();
        //    var dataInitializerService=app.Services.GetRequiredService<IDataInitializer>();
        //    dataInitializerService.Initialize(); 
        //    return app;
        //}
    }
}
