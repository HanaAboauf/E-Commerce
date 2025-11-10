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
        public void Initialize()
        {
            var hasBrands=_StoreDbContext.ProductBrands.Any();
            var hasTypes=_StoreDbContext.ProductTypes.Any();
            var hasProducts=_StoreDbContext.Products.Any();
            if (hasBrands && hasTypes && hasProducts) return;
            try
            {
                if (!hasBrands)
                    DataSeedFromJSON<ProductBrand, int>("brands.json",_StoreDbContext.ProductBrands);
                if (!hasTypes)
                    DataSeedFromJSON<ProductType, int>("types.json",_StoreDbContext.ProductTypes);
                _StoreDbContext.SaveChanges();
                if (!hasProducts)
                    DataSeedFromJSON<Product, int>("products.json",_StoreDbContext.Products);
                _StoreDbContext.SaveChanges();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Filed to seed data {ex}");

            }
        }

        private void DataSeedFromJSON<T, TKey>(string FileName, DbSet<T> dbset) where T: BaseEntity<TKey>
        {

            var path= @"..\E-Commerce.Persistence\Data\DataSeed\JsonFiles\"+ FileName;
            if (!File.Exists(path)) throw new FileNotFoundException($"File {FileName} doesn't exist");
            try
            {
                using var fileStream = File.OpenRead(path);

                var data= JsonSerializer.Deserialize<List<T>>(fileStream, new JsonSerializerOptions()
                {
                   PropertyNameCaseInsensitive = true,
                });

                if (data is null) throw new ArgumentException("There is no data");
                
                dbset.AddRange(data);

            }
            catch (Exception ex) { 
                Console.WriteLine($"Error while reading JSON file {ex.Message}");

            }


        }
    }
}
