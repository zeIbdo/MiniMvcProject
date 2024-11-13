using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniMvcProject.Application.Services.Abstractions;
using MiniMvcProject.Application.UI.ViewModels;
using System.Diagnostics;

namespace MiniMvcProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IBasketService _basketService;
        public HomeController(ICategoryService categoryService, IProductService productService, IBasketService basketService)
        {
            _categoryService = categoryService;
            _productService = productService;
            _basketService = basketService;
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
            await _basketService.AddProductsToCookieBasketAsync(productId);
            var basketItems = _basketService.GetBasket();
            return PartialView("_BasketPartial", basketItems);
        }

        public IActionResult UpdateBasketPartial()
        {
            var basketItems =  _basketService.GetBasket();
            return PartialView("_BasketPartial", basketItems);
        }
    }
}
