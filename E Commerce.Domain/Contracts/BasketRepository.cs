using E_Commerce.Domain.Entities.BasketModule;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Contracts
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer connection) 
        {
            _database=connection.GetDatabase();
        }
        public async Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan timetolive=default)
        {
            var basketJson= JsonSerializer.Serialize(basket);
            var IsCreatedOrUpdated= await _database.StringSetAsync(basket.Id, basketJson,(timetolive==default)? TimeSpan.FromDays(7):timetolive);
            if (IsCreatedOrUpdated)
               return await GetBasketAsync(basket.Id);
            else
                return null;

        }

        public async Task<bool> DeleteBasketAsync(string basketId)=> await _database.KeyDeleteAsync(basketId);
        

        public async Task<CustomerBasket?> GetBasketAsync(string basketId)
        {
            var basket = await _database.StringGetAsync(basketId);
            if (string.IsNullOrEmpty(basket)) return null;
            else
                return JsonSerializer.Deserialize<CustomerBasket>(basket!);
        }
    }
}
