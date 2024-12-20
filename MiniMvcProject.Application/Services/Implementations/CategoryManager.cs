﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MiniMvcProject.Application.Services.Abstractions;
using MiniMvcProject.Application.Services.Implementations.Generic;
using MiniMvcProject.Application.ViewModels.CategoryViewModels;
using MiniMvcProject.Application.ViewModels.Generic;
using MiniMvcProject.Domain.Entities;
using MiniMvcProject.Persistance.Repositories.Abstractions.Generic;

namespace MiniMvcProject.Application.Services.Implementations;


public class CategoryManager : CrudManager<Category, CategoryViewModel, CategoryCreateViewModel, CategoryUpdateViewModel>, ICategoryService
{
    private readonly IRepository<Category> _categoryRepository;
    public CategoryManager(IRepository<Category> repository, IMapper mapper) : base(repository, mapper)
    {
        _categoryRepository = repository;
    }

    public override async Task<ResultViewModel<CategoryViewModel>> CreateAsync(CategoryCreateViewModel createViewModel)
    {
        var validationResult = await _validateParentId(createViewModel.ParentId);
        if (validationResult != null)
        {
            return validationResult;
        }


        return await base.CreateAsync(createViewModel);
    }

    public override async Task<ResultViewModel<CategoryViewModel>> UpdateAsync(CategoryUpdateViewModel vm)
    {
        var validationResult = await _validateParentId(vm.ParentId);
        if (validationResult != null)
        {
            return validationResult;
        }

        return await base.UpdateAsync(vm);
    }

    private async Task<ResultViewModel<CategoryViewModel>?> _validateParentId(int? parentId)
    {
        if (parentId < 0)
        {
            return new ResultViewModel<CategoryViewModel>
            {
                Success = false,
                Message = "Parent ID is invalid."
            };
        }
        var existCategory = await _categoryRepository.GetAsync(x => x.Id == parentId, include: x => x.Include(x => x.SubCategories));

        if (existCategory is null)
        {
            return new ResultViewModel<CategoryViewModel>
            {
                Success = false,
                Message = "There is no Category with that ID."
            };
        }
        if (existCategory!.SubCategories.Count == 0)
            return new ResultViewModel<CategoryViewModel>
            {
                Success = false,
                Message = "Cannnot give parent Id to sub category."
            };
        return null;
    }
}
