using MiniMvcProject.Application.Services.Abstractions.Generic;
using MiniMvcProject.Application.ViewModels.BasketItemViewModels;
using MiniMvcProject.Domain.Entities;

namespace MiniMvcProject.Application.Services.Abstractions;

public interface IBasketItemService : ICrudService<BasketItem, BasketItemViewModel, BasketItemCreateViewModel, BasketItemUpdateViewModel>
{
    Task<List<BasketItemViewModel>> GetBasketAsync();
}
