using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MiniMvcProject.Application.Services.Abstractions;
using MiniMvcProject.Application.Services.Implementations.Generic;
using MiniMvcProject.Application.UI.ViewModels;
using MiniMvcProject.Application.Utilities;
using MiniMvcProject.Application.ViewModels.BasketItemViewModels;
using MiniMvcProject.Domain.Entities;
using MiniMvcProject.Persistance.Repositories.Abstractions.Generic;
using Newtonsoft.Json;
using System.Security.Claims;

namespace MiniMvcProject.Application.Services.Implementations;

public class BasketItemManager : CrudManager<BasketItem, BasketItemViewModel, BasketItemCreateViewModel, BasketItemUpdateViewModel>, IBasketItemService
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IRepository<BasketItem> _repository;
    private readonly IMapper _mapper;
    private readonly IProductService _productService;
    public BasketItemManager(IRepository<BasketItem> repository, IMapper mapper, IHttpContextAccessor contextAccessor, IProductService productService) : base(repository, mapper)
    {
        _contextAccessor = contextAccessor;
        _repository = repository;
        _mapper = mapper;
        _productService = productService;
    }

    public async Task<List<BasketItemViewModel>> GetBasketAsync()
    {
        
        
            var userId = _getUserId();


            var basketItems = await _repository.GetListAsync(x => x.AppUserId == userId,include:x=>x.Include(x=>x.Product));

            var vms = _mapper.Map<List<BasketItemViewModel>>(basketItems);

            return vms;
       

    }
    private List<BasketViewModel> _readBasketFromCookie()
    {
        string json = _contextAccessor.HttpContext.Request.Cookies[Constants.PUSKAT_BASKET_KEY] ?? "";

        var basketItems = JsonConvert.DeserializeObject<List<BasketViewModel>>(json) ?? new();
        return basketItems;
    }


    private string _getUserId()
    {
        return _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";
    }


    private bool _checkAuthorized()
    {
        return _contextAccessor.HttpContext.User.Identity?.IsAuthenticated ?? false;
    }

    public async Task<List<BasketItemViewModel>> GetBasketAsync(List<BasketItemViewModel> models)
    {
       
            var dtos = models;  


            foreach (var dto in dtos)
            {
                var product = await _productService.GetAsync(dto.ProductId);

                if (product.Data is null)
                {
                    dtos.Remove(dto);
                    continue;
                }
                //dto.Product = product.Data;
            }


            return dtos;
        
    }
}
