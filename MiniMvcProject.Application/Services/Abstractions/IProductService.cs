using MiniMvcProject.Application.Services.Abstractions.Generic;
using MiniMvcProject.Application.ViewModels.Generic;
using MiniMvcProject.Application.ViewModels.ProductViewModels;
using MiniMvcProject.Domain.Entities;

namespace MiniMvcProject.Application.Services.Abstractions
{
    public interface IProductService : ICrudService<Product, ProductViewModel, ProductCreateViewModel, ProductUpdateViewModel>
    {
        Task<ProductCreateViewModel> GetProductCreateViewModelAsync(ProductCreateViewModel productCreateViewModel);
        Task<ResultViewModel<ProductUpdateViewModel>> GetProductUpdateViewModelAsync(int id);
        Task<ResultViewModel<ProductViewModel>> SoftDeleteProductAsync(int id);     
    }
}
