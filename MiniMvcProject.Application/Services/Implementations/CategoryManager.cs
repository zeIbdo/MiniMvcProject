using AutoMapper;
using MiniMvcProject.Application.Services.Abstractions;
using MiniMvcProject.Application.Services.Implementations.Generic;
using MiniMvcProject.Application.ViewModels.CategoryViewModels;
using MiniMvcProject.Domain.Entities;
using MiniMvcProject.Persistance.Repositories.Abstractions.Generic;

namespace MiniMvcProject.Application.Services.Implementations
{

    public class CategoryManager : CrudManager<Category, CategoryViewModel, CategoryCreateViewModel, CategoryUpdateViewModel>, ICategoryService
    {
        public CategoryManager(IRepository<Category> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }


}
