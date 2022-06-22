using Console.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Console.ApplicationCore.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> Table { get; }

        Task DeleteAsync(T entity);

        Task DeleteAsync(IList<T> entities);

        Task<T> GetAsync(Expression<Func<T, bool>> predicate);

        Task<T> GetByIdAsync(int id);

        Task<IList<T>> GetByIdsAsync(List<int> ids);

        Task InsertAsync(T entity);

        Task InsertAsync(IList<T> entities);

        Task UpdateAsync(T entity);
    }
}
