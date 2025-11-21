
using E_Commerce.Domain.Contracts;
using E_Commerce.Persistence.Data.Contexts;
using E_Commerce.Persistence.Data.DataSeed;
using E_Commerce.Persistence.Repositories;
using E_Commerce.Services;
using E_Commerce.Services.Profiles;
using E_Commerce.Services_Abstraction;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            #region Add service to container
            builder.Services.AddControllers();
            builder.Services.AddDbContext<StoreDbContext>(optios =>
            {
                optios.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddAutoMapper(typeof(ServicesAssemblyReference).Assembly);
            builder.Services.AddScoped<IDataInitializer,DataInitializer>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IProductService, ProductService>();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            //builder.Services.AddOpenApi();
            builder.Services.AddSwaggerGen();

            #endregion


            var app = builder.Build();

            #region DataSeeding
            using var scope = app.Services.CreateScope();
            var dbService = scope.ServiceProvider.GetRequiredService<StoreDbContext>();
            if (dbService.Database.GetPendingMigrations().Any()) dbService.Database.Migrate();
            var dataInitializerService = scope.ServiceProvider.GetRequiredService<IDataInitializer>();
            dataInitializerService.InitializeAsync();

            #endregion

            #region Middlewares
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                //app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers(); 
            #endregion

            app.Run();
        }
    }
}
