using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using E_Commerce.Persistence.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _StoreDbContext;
        private readonly Dictionary<Type,object>_repositories=[];

        public UnitOfWork(StoreDbContext storeDbContext)
        {
            _StoreDbContext = storeDbContext;
        }

        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var entityType = typeof(TEntity);
            if (_repositories.TryGetValue(entityType, out var repository))
                return (IGenericRepository<TEntity, TKey>) repository;
            var newRepository = new GenericRepository<TEntity, TKey>(_StoreDbContext);
            _repositories[entityType] = newRepository;
            return newRepository;

        }

        public async Task<int> SaveChangesAsync()=> await _StoreDbContext.SaveChangesAsync();
    }
}
