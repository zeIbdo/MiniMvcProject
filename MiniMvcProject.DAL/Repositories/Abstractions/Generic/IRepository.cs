using core.Persistance.Paging;
using Microsoft.EntityFrameworkCore.Query;
using MiniMvcProject.Domain.Entities;
using System.Linq.Expressions;

namespace MiniMvcProject.Persistance.Repositories.Abstractions.Generic
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T?> GetAsync(int id);

        Task<T?> GetAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, bool enableTracking = true);

        Task<Paginate<T>> GetListAsync(Expression<Func<T, bool>>? predicate = null,
                                        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                                        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
                                        int index = 0, int size = int.MaxValue, bool enableTracking = true);

        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(T entity);
        Task<int> SaveChangesAsync();
    }
}
