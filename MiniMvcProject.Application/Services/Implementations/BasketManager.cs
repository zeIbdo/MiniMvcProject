using Microsoft.AspNetCore.Http;
using MiniMvcProject.Application.Services.Abstractions;
using MiniMvcProject.Application.Utilities;
using MiniMvcProject.Application.UI.ViewModels;
using Newtonsoft.Json;

namespace MiniMvcProject.Application.Services.Implementations
{
    public class BasketManager : IBasketService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IProductService _productService;
        private readonly string BASKET_KEY;

        public BasketManager(IHttpContextAccessor contextAccessor, IProductService productService)
        {
            _contextAccessor = contextAccessor;
            _productService = productService;
            BASKET_KEY = Constants.PUSKAT_BASKET_KEY;
        }

        public async Task<string> AddProductsToCookieBasketAsync(int productId)
        {
            var productResult= await _productService.GetAsync(productId);

            var basket = _contextAccessor.HttpContext.Request.Cookies[BASKET_KEY];

            List<BasketViewModel> basketVms = new List<BasketViewModel>();

            if (basket != null)
            {
                basketVms = JsonConvert.DeserializeObject<List<BasketViewModel>>(basket) ?? new();
            }

            var exists = basketVms.FirstOrDefault(x => x.Product.Id == productId);

            if (exists != null)
                exists.Count++;
            else
            {
                BasketViewModel basketViewModel = new BasketViewModel()
                {
                    Count = 1,
                    Product = productResult.Data
                };
                basketVms.Add(basketViewModel);
            }

            var json = JsonConvert.SerializeObject(basketVms);

            _contextAccessor.HttpContext.Response.Cookies.Append(BASKET_KEY, json);

            return json;
        }

        public Task<string> AddProductsToDbBasketAsync(int productId)
        {
            throw new NotImplementedException();
        }

        public List<BasketViewModel> GetBasket()
        {
            var basket = _contextAccessor.HttpContext.Request.Cookies[BASKET_KEY];

            var basketVms = string.IsNullOrEmpty(basket)
            ? new List<BasketViewModel>()
            : JsonConvert.DeserializeObject<List<BasketViewModel>>(basket) ?? new List<BasketViewModel>();

            return basketVms;
        }
    }
}
