using AutoMapper;
using MiniMvcProject.Application.Services.Abstractions;
using MiniMvcProject.Application.Services.Implementations.Generic;
using MiniMvcProject.Application.ViewModels.ProductImageViewModels;
using MiniMvcProject.Domain.Entities;
using MiniMvcProject.Persistance.Repositories.Abstractions.Generic;

namespace MiniMvcProject.Application.Services.Implementations
{
    public class ProductImageManager : CrudManager<ProductImage, ProductImageViewModel, ProductImageCreateViewModel, ProductImageUpdateViewModel>, IProductImageService
    {
        public ProductImageManager(IRepository<ProductImage> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }


}
