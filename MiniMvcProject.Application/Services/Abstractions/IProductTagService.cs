using MiniMvcProject.Application.Services.Abstractions.Generic;
using MiniMvcProject.Application.ViewModels.ProductImageViewModels;
using MiniMvcProject.Application.ViewModels.ProductTagViewModels;
using MiniMvcProject.Domain.Entities;

namespace MiniMvcProject.Application.Services.Abstractions
{
    public interface IProductTagService : ICrudService<ProductTag, ProductTagViewModel, ProductTagCreateViewModel, ProductTagUpdateViewModel>
    {
    }
}
