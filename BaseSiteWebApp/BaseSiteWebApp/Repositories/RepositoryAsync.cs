using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BaseSiteWebApp.Interfaces;
using BaseSiteWebApp.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace BaseSiteWebApp.Repositories
{
    public class RepositoryAsync<T> : IRepositoryAsync<T> where T : class
    {
        protected readonly NorthwindContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public RepositoryAsync(NorthwindContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }
        public async Task<IEnumerable<T>> GetAllAsync(Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, int take = 0)
        {
            IQueryable<T> query = _dbSet;
            if (include != null)
                query = include(query);
            if (take > 0)
                query = query.Take(take);

            return await query.ToListAsync();
        }
        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = _dbSet;
            if (predicate != null)
                query = query.Where(predicate);
            if (include != null)
                query = include(query);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }
        public async Task Create(T entity)
        {
            await _dbContext.AddAsync(entity);
            await SaveAsync();
        }
        public async Task Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await SaveAsync();
        }
        public async Task Delete(T entity)
        {
            _dbContext.Remove(entity);
            await SaveAsync();
        }

        protected async Task SaveAsync() => await _dbContext.SaveChangesAsync();
    }
}