using AutoMapper;
using MiniMvcProject.Application.Services.Abstractions;
using MiniMvcProject.Application.Services.Implementations.Generic;
using MiniMvcProject.Application.ViewModels.BasketItemViewModels;
using MiniMvcProject.Domain.Entities;
using MiniMvcProject.Persistance.Repositories.Abstractions.Generic;

namespace MiniMvcProject.Application.Services.Implementations
{
    public class BasketItemManager : CrudManager<BasketItem, BasketItemViewModel, BasketItemCreateViewModel, BasketItemUpdateViewModel>, IBasketItemService
    {
        public BasketItemManager(IRepository<BasketItem> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }


}
