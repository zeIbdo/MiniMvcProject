using core.Persistance.Paging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using MiniMvcProject.DAL.DataContext;
using MiniMvcProject.Domain.Entities;
using MiniMvcProject.Persistance.Repositories.Abstractions;
using MiniMvcProject.Persistance.Repositories.Implementations.Generic;
using System.Linq.Expressions;

namespace MiniMvcProject.Persistance.Repositories.Implementations
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Paginate<Product>> GetPaginatedProductsAsync(Expression<Func<Product, bool>>? predicate = null, Func<IQueryable<Product>, IIncludableQueryable<Product, object>>? include = null, Func<IQueryable<Product>, IOrderedQueryable<Product>>? orderBy = null, int index = 0, int size = int.MaxValue, bool enableTracking = true)
        {
            var query = _context.Products.AsQueryable();
            if (predicate != null)
                query = query.Where(predicate);

            if (include != null)
                query = include(query);

            if (orderBy != null)
                query = orderBy(query);

            if (!enableTracking)
                query = query.AsNoTracking();

            return await query.ToPaginateAsync(index,size);
        }

        public async Task SoftDeleteProductAsync(int id)
        {
            
            var product = await GetAsync(id);
            if(product!=null) 
                product.IsDeleted= true;

            await _context.SaveChangesAsync();
        }
    }
}
