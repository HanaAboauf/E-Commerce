using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Entities.ProductModule;
using E_Commerce.Persistence.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Data.DataSeed
{
    public class DataInitializer : IDataInitializer
    {
        private readonly StoreDbContext _StoreDbContext;

        public DataInitializer(StoreDbContext storeDbContext)
        {
            _StoreDbContext = storeDbContext;
        }
        public async Task InitializeAsync()
        {
            var hasBrands=await _StoreDbContext.ProductBrands.AnyAsync();
            var hasTypes=await _StoreDbContext.ProductTypes.AnyAsync();
            var hasProducts=await _StoreDbContext.Products.AnyAsync();
            if (hasBrands && hasTypes && hasProducts) return;
            try
            {
                if (!hasBrands)
                   await DataSeedFromJSONAsync<ProductBrand, int>("brands.json",_StoreDbContext.ProductBrands);
                if (!hasTypes)
                  await DataSeedFromJSONAsync<ProductType, int>("types.json",_StoreDbContext.ProductTypes);
               await _StoreDbContext.SaveChangesAsync();
                if (!hasProducts)
                   await DataSeedFromJSONAsync<Product, int>("products.json",_StoreDbContext.Products);
               await _StoreDbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Filed to seed data {ex}");

            }
        }

        private async Task DataSeedFromJSONAsync<T, TKey>(string FileName, DbSet<T> dbset) where T: BaseEntity<TKey>
        {

            var path= @"..\E-Commerce.Persistence\Data\DataSeed\JsonFiles\"+ FileName;
            if (!File.Exists(path)) throw new FileNotFoundException($"File {FileName} doesn't exist");
            try
            {
                using var fileStream = File.OpenRead(path);

                var data=await JsonSerializer.DeserializeAsync<List<T>>(fileStream, new JsonSerializerOptions()
                {
                   PropertyNameCaseInsensitive = true,
                });

                if (data is null) throw new ArgumentException("There is no data");
                
               await dbset.AddRangeAsync(data);

            }
            catch (Exception ex) { 
                Console.WriteLine($"Error while reading JSON file {ex.Message}");

            }


        }
    }
}
