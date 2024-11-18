using core.Persistance.Paging;
using Microsoft.EntityFrameworkCore.Query;
using MiniMvcProject.Application.Services.Abstractions.Generic;
using MiniMvcProject.Application.ViewModels.Generic;
using MiniMvcProject.Application.ViewModels.ProductViewModels;
using MiniMvcProject.Domain.Entities;
using System.Linq.Expressions;

namespace MiniMvcProject.Application.Services.Abstractions
{
    public interface IProductService : ICrudService<Product, ProductViewModel, ProductCreateViewModel, ProductUpdateViewModel>
    {
        Task<ProductCreateViewModel> GetProductCreateViewModelAsync(ProductCreateViewModel productCreateViewModel);
        Task<ResultViewModel<ProductUpdateViewModel>> GetProductUpdateViewModelAsync(int id);
        Task<ResultViewModel<ProductViewModel>> SoftDeleteProductAsync(int id);
        Task IncrementViewCountAsync(int id);
        Task<Paginate<ProductViewModel>> GetPaginatedProductsAsync(Expression<Func<Product, bool>>? predicate = null,
                Func<IQueryable<Product>, IIncludableQueryable<Product, object>>? include = null,
                Func<IQueryable<Product>, IOrderedQueryable<Product>>? orderBy = null,
                int index = 0,
                int size = int.MaxValue);
    }
}
