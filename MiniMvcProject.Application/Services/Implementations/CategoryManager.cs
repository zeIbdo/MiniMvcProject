using AutoMapper;
using MiniMvcProject.Application.Services.Abstractions;
using MiniMvcProject.Application.Services.Implementations.Generic;
using MiniMvcProject.Application.ViewModels.CategoryViewModels;
using MiniMvcProject.Application.ViewModels.Generic;
using MiniMvcProject.Domain.Entities;
using MiniMvcProject.Persistance.Repositories.Abstractions.Generic;

namespace MiniMvcProject.Application.Services.Implementations;


public class CategoryManager : CrudManager<Category, CategoryViewModel, CategoryCreateViewModel, CategoryUpdateViewModel>, ICategoryService
{
    public CategoryManager(IRepository<Category> repository, IMapper mapper) : base(repository, mapper)
    {
    }

    public override async Task<ResultViewModel<CategoryViewModel>> CreateAsync(CategoryCreateViewModel createViewModel)
    {
        var validationResult = _validateParentId(createViewModel.ParentId);
        if (validationResult != null)
        {
            return validationResult;
        }

        return await base.CreateAsync(createViewModel);
    }

    public override async Task<ResultViewModel<CategoryViewModel>> UpdateAsync(CategoryUpdateViewModel vm)
    {
        var validationResult = _validateParentId(vm.ParentId);
        if (validationResult != null)
        {
            return validationResult; 
        }

        return await base.UpdateAsync(vm);
    }

    private ResultViewModel<CategoryViewModel>? _validateParentId(int? parentId)
    {
        if (parentId < 0)
        {
            return new ResultViewModel<CategoryViewModel>
            {
                Success = false,
                Message = "Parent ID cannot be a negative number."
            };
        }
        return null; 
    }
}
