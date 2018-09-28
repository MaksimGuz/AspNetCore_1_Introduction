using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BaseSiteWebApp.Interfaces;
using BaseSiteWebApp.Models;
using System.Threading.Tasks;

namespace BaseSiteWebApp.Repositories
{
    public class RepositoryAsync<T> : IRepositoryAsync<T> where T : class
    {
        protected readonly DbContext _dbContext;

        public RepositoryAsync(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
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