﻿using Microsoft.AspNetCore.Http;
using MiniMvcProject.Application.Services.Abstractions;
using MiniMvcProject.Application.Utilities;
using MiniMvcProject.Application.UI.ViewModels;
using Newtonsoft.Json;
using AutoMapper;
using MiniMvcProject.Application.ViewModels.BasketItemViewModels;
using MiniMvcProject.Domain.Entities;
using System.Security.Claims;

namespace MiniMvcProject.Application.Services.Implementations
{
    public class BasketManager : IBasketService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IProductService _productService;
        private readonly IBasketItemService _basketItemService;
        private readonly IMapper _mapper;
        private readonly string BASKET_KEY;

        public BasketManager(IHttpContextAccessor contextAccessor, IProductService productService, IBasketItemService basketItemService, IMapper mapper)
        {
            _contextAccessor = contextAccessor;
            _productService = productService;
            BASKET_KEY = Constants.PUSKAT_BASKET_KEY;
            _basketItemService = basketItemService;
            _mapper = mapper;
        }

        public async Task<List<BasketItemViewModel>> AddProductsToCookieBasketAsync(int productId)
        {
            var productResult = await _productService.GetAsync(productId);

            var basket = _contextAccessor.HttpContext.Request.Cookies[BASKET_KEY];

            List<BasketViewModel> basketVms = new List<BasketViewModel>();

            if (basket != null)
            {
                basketVms = JsonConvert.DeserializeObject<List<BasketViewModel>>(basket) ?? new();
            }

            var exists = basketVms.FirstOrDefault(x => x.ProductId == productId);

            if (exists != null)
                exists.Count++;
            else
            {
                BasketViewModel basketViewModel = new BasketViewModel()
                {
                    Count = 1,
                    ProductId = productId,
                    ProductName = productResult.Data!.Name!,
                    ProductPrice = productResult.Data.MainPrice,
                    //ProductImageUrl = productResult.Data.Images.FirstOrDefault(x => x.IsMain == true).ImageUrl
                };
                basketVms.Add(basketViewModel);
            }

            var json = JsonConvert.SerializeObject(basketVms);

            _contextAccessor.HttpContext.Response.Cookies.Append(BASKET_KEY, json);

            return _mapper.Map<List<BasketItemViewModel>>(basketVms);

        }
        public async Task<List<BasketItemViewModel>> DecreaseProductsToCookieBasketAsync(int productId)
        {
            var productResult = await _productService.GetAsync(productId);

            var basket = _contextAccessor.HttpContext.Request.Cookies[BASKET_KEY];

            List<BasketViewModel> basketVms = new List<BasketViewModel>();

            if (basket != null)
            {
                basketVms = JsonConvert.DeserializeObject<List<BasketViewModel>>(basket) ?? new();
            }

            var exists = basketVms.FirstOrDefault(x => x.ProductId == productId);

            if(exists.Count==1)
            {
                //BasketViewModel basketViewModel = new BasketViewModel()
                //{
                //    Count = 1,
                //    ProductId = productId,
                //    ProductName = productResult.Data!.Name!,
                //    ProductPrice = productResult.Data.MainPrice,
                //    //ProductImageUrl = productResult.Data.Images.FirstOrDefault(x => x.IsMain == true).ImageUrl
                //};
                //basketVms.Add(basketViewModel);
                throw new Exception();
            }
            if (exists != null)
                exists.Count--;

            var json = JsonConvert.SerializeObject(basketVms);

            _contextAccessor.HttpContext.Response.Cookies.Append(BASKET_KEY, json);

            return _mapper.Map<List<BasketItemViewModel>>(basketVms);

        }

        public async Task AddProductsToDbBasketAsync(int productId)
        {
            var userId = _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var basketItemResult = await _basketItemService.GetAsync(x => x.ProductId == productId && x.AppUserId == userId,
                enableTracking: false);

            if (basketItemResult.Data != null)
            {
                var viewModel = _mapper.Map<BasketItemUpdateViewModel>(basketItemResult.Data);
                viewModel.Count++;
                await _basketItemService.UpdateAsync(viewModel);
            }
            else
            {
                BasketItemCreateViewModel basketItemCreateViewModel = new BasketItemCreateViewModel
                {
                    ProductId = productId,
                    Count = 1,
                    AppUserName = userId!
                };
                await _basketItemService.CreateAsync(basketItemCreateViewModel);

            }
        }
        public async Task DecreaseProductsToDbBasketAsync(int productId)
        {
            var userId = _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var basketItemResult = await _basketItemService.GetAsync(x => x.ProductId == productId && x.AppUserId == userId,
                enableTracking: false);

            if(basketItemResult.Data.Count==1)
            {
                throw new Exception();

            }
            if (basketItemResult.Data != null)
            {
                var viewModel = _mapper.Map<BasketItemUpdateViewModel>(basketItemResult.Data);
                viewModel.Count--;
                await _basketItemService.UpdateAsync(viewModel);
            }
        }

        public async Task AddToBasketAsync(int productId)
        {
            if (!_contextAccessor.HttpContext.User.Identity!.IsAuthenticated)
            {
                await AddProductsToCookieBasketAsync(productId);
            }
            else
            {
                await AddProductsToDbBasketAsync(productId);
            }

        }
        public async Task DecreaseBasketItemAsync(int productId)
        {
            if (!_contextAccessor.HttpContext.User.Identity!.IsAuthenticated)
            {
                await DecreaseProductsToCookieBasketAsync(productId);
            }
            else
            {
                await AddProductsToDbBasketAsync(productId);
               
            }

        }

        //public async Task UpdateDbBasketAsync(int productId)
        //{
        //    var userId = _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    var basketItemResult = await _basketItemService.GetAsync(x => x.ProductId == productId && x.AppUserId == userId,
        //        enableTracking: false);
        //    if (basketItemResult.Data != null)
        //    {

        //    }


        //}


        public async Task<bool> DeleteFromDB(int productId)
        {

            var userId = _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var basketItemResult = await _basketItemService.GetAsync(x => x.ProductId == productId && x.AppUserId == userId,
                enableTracking: false);
            if(!basketItemResult.Success)
                return false;
            var result = await _basketItemService.RemoveAsync(basketItemResult.Data.Id);
            return true;
            


        }
        public async Task<List<BasketItemViewModel>> DeleteFromCookie(int productId)
        {
            var productResult = await _productService.GetAsync(productId);

            var basket = _contextAccessor.HttpContext.Request.Cookies[BASKET_KEY];

            List<BasketViewModel> basketVms = new List<BasketViewModel>();

            if (basket != null)
            {
                basketVms = JsonConvert.DeserializeObject<List<BasketViewModel>>(basket) ?? new();
            }
            

            var exists = basketVms.FirstOrDefault(x => x.ProductId == productId);
            if (exists != null)
            {
                basketVms.Remove(exists);
                var json = JsonConvert.SerializeObject(basketVms);

                _contextAccessor.HttpContext.Response.Cookies.Append(BASKET_KEY, json);

                return _mapper.Map<List<BasketItemViewModel>>(basketVms);
            }
            return _mapper.Map<List<BasketItemViewModel>>(basketVms);


        }

    }
}
