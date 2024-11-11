using MiniMvcProject.Application.Services.Abstractions.Generic;
using MiniMvcProject.Application.ViewModels.ProductImageViewModels;
using MiniMvcProject.Domain.Entities;

namespace MiniMvcProject.Application.Services.Abstractions
{
    public interface IProductImageService : ICrudService<ProductImage, ProductImageViewModel, ProductImageCreateViewModel, ProductImageUpdateViewModel>
    {
    }
}
