using AutoMapper;
using MiniMvcProject.Application.Services.Abstractions;
using MiniMvcProject.Application.Services.Implementations.Generic;
using MiniMvcProject.Application.ViewModels.ProductTagViewModels;
using MiniMvcProject.Domain.Entities;
using MiniMvcProject.Persistance.Repositories.Abstractions.Generic;

namespace MiniMvcProject.Application.Services.Implementations
{
    public class ProductTagManager : CrudManager<ProductTag, ProductTagViewModel, ProductTagCreateViewModel, ProductTagUpdateViewModel>, IProductTagService
    {
        public ProductTagManager(IRepository<ProductTag> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }


}
