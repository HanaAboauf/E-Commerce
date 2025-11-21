using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using E_Commerce.Persistence.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Repositories
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly StoreDbContext _StoreDbContext;

        public GenericRepository(StoreDbContext storeDbContext)
        {
            _StoreDbContext = storeDbContext;
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync()=> await _StoreDbContext.Set<TEntity>().ToListAsync();
        public async Task<TEntity?> GetByIdAsync(TKey id)=> await _StoreDbContext.Set<TEntity>().FindAsync(id);
        public async Task AddAsync(TEntity entity)=> await _StoreDbContext.Set<TEntity>().AddAsync(entity);
        public void DeleteAsync(TEntity entity)=> _StoreDbContext.Set<TEntity>().Remove(entity);
        public void UpdateAsync(TEntity entity)=> _StoreDbContext.Set<TEntity>().Update(entity);

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, TKey> specifications)
        {
           return await SpecificationEvaluator.CreatetQuery(_StoreDbContext.Set<TEntity>(), specifications).ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(ISpecifications<TEntity, TKey> specifications)
        {
            return await SpecificationEvaluator.CreatetQuery(_StoreDbContext.Set<TEntity>(), specifications).FirstOrDefaultAsync();
        }
    }
}
