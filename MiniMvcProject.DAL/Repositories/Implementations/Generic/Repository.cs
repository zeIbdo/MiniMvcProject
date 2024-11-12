using core.Persistance.Paging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using MiniMvcProject.DAL.DataContext;
using MiniMvcProject.Domain.Entities;
using MiniMvcProject.Persistance.Repositories.Abstractions.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MiniMvcProject.Persistance.Repositories.Implementations.Generic
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<T> AddAsync(T entity)
        {
            var entityEntry = await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();

            return entityEntry.Entity;
        }

        public async Task<T> DeleteAsync(T entity)
        {
            var entityEntry = _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();

            return entityEntry.Entity;
        }



        public async Task<T?> GetAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T?> GetAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
        {
            var query = _context.Set<T>().AsQueryable();

            if (include != null) query = include(query);

            return await query.FirstOrDefaultAsync(predicate);
        }


        public async Task<Paginate<T>> GetListAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, int index = 0, int size = int.MaxValue, bool enableTracking = true)
        {
            IQueryable<T> queryable = _context.Set<T>();

            if (!enableTracking) queryable = queryable.AsNoTracking();

            if (include != null) queryable = include(queryable);

            if (predicate != null) queryable = queryable.Where(predicate);

            if (orderBy != null) queryable = orderBy(queryable);

            return await queryable.ToPaginateAsync(index, size);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            var entityEntry = _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();

            return entityEntry.Entity;
        }
    }
}
