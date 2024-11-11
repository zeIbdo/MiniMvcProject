using AutoMapper;
using MiniMvcProject.Application.Services.Abstractions;
using MiniMvcProject.Application.Services.Implementations.Generic;
using MiniMvcProject.Application.ViewModels.ProductViewModels;
using MiniMvcProject.Domain.Entities;
using MiniMvcProject.Persistance.Repositories.Abstractions.Generic;

namespace MiniMvcProject.Application.Services.Implementations
{
    public class ProductManager : CrudManager<Product, ProductViewModel, ProductCreateViewModel, ProductUpdateViewModel>, IProductService
    {
        public ProductManager(IRepository<Product> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }


}
