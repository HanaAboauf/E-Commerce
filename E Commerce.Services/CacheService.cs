using E_Commerce.Domain.Contracts;
using E_Commerce.Services_Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Services
{
    public class CacheService : ICacheService
    {
        private readonly ICacheRepository _CacheRepository;

        public CacheService(ICacheRepository cacheRepository)
        {
            _CacheRepository = cacheRepository;
        }

        public async Task<string?> GetAsync(string CacheKey)
        {
            return await _CacheRepository.GetAsync(CacheKey);
        }

        public async Task SetAsync(string CacheKey, object cachedValue, TimeSpan TimeToLive)
        {
            var serializedValue = JsonSerializer.Serialize(cachedValue);

            await _CacheRepository.SetAsync(CacheKey, serializedValue, TimeToLive);
        }
    }
}
