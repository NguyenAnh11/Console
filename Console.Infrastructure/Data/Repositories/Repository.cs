using Console.ApplicationCore.Entities;
using Console.ApplicationCore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Console.Infrastructure.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<T> Table => _context.Set<T>();

        public async Task DeleteAsync(T entity)
        {
            if(entity == null)
                throw new ArgumentNullException(nameof(entity));    

            _context.Set<T>().Remove(entity);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(IList<T> entities)
        {
            if(entities == null)
                throw new ArgumentNullException(nameof(entities));

            if (!entities.Any())
                return;

            foreach(var entity in entities)
                await DeleteAsync(entity);  
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return await Table.FirstOrDefaultAsync(predicate);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            if (id == 0)
                return null;

            return await Table.FirstOrDefaultAsync(p => p.Id == id);    
        }

        public async Task<IList<T>> GetByIdsAsync(List<int> ids)
        {
            if (ids == null)
                throw new ArgumentNullException(nameof(ids));

            if (!ids.Any())
                return null;

            if (ids.Contains(0))
                ids.RemoveAll(p => p == 0);

            return await Table
                .Where(p => ids.Contains(p.Id))
                .ToListAsync();
        }

        public async Task InsertAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            await _context.Set<T>().AddAsync(entity);

            await _context.SaveChangesAsync();
        }

        public async Task InsertAsync(IList<T> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            if (!entities.Any())
                return;

            foreach (var entity in entities)
                await InsertAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _context.Set<T>().Update(entity);

            await _context.SaveChangesAsync();
        }
    }
}
