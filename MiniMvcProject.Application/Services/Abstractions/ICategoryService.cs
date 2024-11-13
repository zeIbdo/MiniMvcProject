using MiniMvcProject.Application.Services.Abstractions.Generic;
using MiniMvcProject.Application.ViewModels.CategoryViewModels;
using MiniMvcProject.Domain.Entities;

namespace MiniMvcProject.Application.Services.Abstractions
{
    public interface ICategoryService : ICrudService<Category,CategoryViewModel, CategoryCreateViewModel, CategoryUpdateViewModel>
    {

    }
}
