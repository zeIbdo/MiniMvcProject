using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniMvcProject.Application.Services.Abstractions;
using MiniMvcProject.Application.UI.ViewModels;
using MiniMvcProject.Application.ViewModels.BasketItemViewModels;
using System.Diagnostics;

namespace MiniMvcProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IBasketService _basketService;
        private readonly IBasketItemService _basketItemService;
        private readonly IMapper _mapper;
        public HomeController(ICategoryService categoryService, IProductService productService, IBasketService basketService, IBasketItemService basketItemService, IMapper mapper)
        {
            _categoryService = categoryService;
            _productService = productService;
            _basketService = basketService;
            _basketItemService = basketItemService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var resultCategories = await _categoryService.GetListAsync(predicate: x => x.SubCategories.Count == 0 && x.ParentCategoryId == null, index: 0, size: 3);

            return View(new HomeViewModel { TopThreeCategories = resultCategories.Data!.ToList() });
        }


        public async Task<IActionResult> RelatedProducts(int categoryId)
        {
            var productsResult = await _productService.GetListAsync(predicate: x => x.CategoryId == categoryId, include: p => p.Include(product => product.ProductImages));


            return PartialView("_RelatedProducts", productsResult.Data!.ToList());
        }


        public async Task<IActionResult> AddToBasket(int productId)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                await _basketService.AddProductsToDbBasketAsync(productId);
                var basketDbItems = await _basketItemService.GetBasketAsync();
                return PartialView("_BasketDbPartial", basketDbItems);

            }
            else
            {
                var vms = await _basketService.AddProductsToCookieBasketAsync(productId);
                var basketDbItems = await _basketItemService.GetBasketAsync(vms);
                return PartialView("_BasketDbPartial", basketDbItems);
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetBasket()
        {
            var basketItems = await _basketItemService.GetBasketAsync();
            return PartialView("_BasketDbPartial", basketItems);
        }
    }
}
