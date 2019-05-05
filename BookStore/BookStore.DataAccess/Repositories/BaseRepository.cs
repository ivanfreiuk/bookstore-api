using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BookStore.DataAccess.Context;
using BookStore.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DataAccess.Repositories
{
    public class BaseRepository<TEntity> : IDisposable, IGenericRepository<TEntity> where TEntity: class
    {
        protected readonly StoreDbContext _context;

        public BaseRepository(StoreDbContext context)
        {
            _context = context;
        }
        
        public virtual async Task<TEntity> GetAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task<ICollection<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public virtual async Task<ICollection<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }

        public virtual async Task AddRangeAsync(ICollection<TEntity> entities)
        {
           await _context.Set<TEntity>().AddRangeAsync(entities);
        }

        public virtual async Task RemoveAsync(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public virtual async Task RemoveRangeAsync(ICollection<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
