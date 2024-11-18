using core.Persistance.Paging;
using Microsoft.EntityFrameworkCore.Query;
using MiniMvcProject.Domain.Entities;
using MiniMvcProject.Persistance.Repositories.Abstractions.Generic;
using System.Linq.Expressions;

namespace MiniMvcProject.Persistance.Repositories.Abstractions
{
    public interface IProductRepository : IRepository<Product>
    {
        Task SoftDeleteProductAsync(int id);
        Task<Paginate<Product>> GetPaginatedProductsAsync(Expression<Func<Product, bool>>? predicate = null,
                Func<IQueryable<Product>, IIncludableQueryable<Product, object>>? include = null,
                Func<IQueryable<Product>, IOrderedQueryable<Product>>? orderBy = null,
                int index = 0,
                int size = int.MaxValue,
                bool enableTracking = true);
    }
}
