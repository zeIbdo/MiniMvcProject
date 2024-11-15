using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniMvcProject.Application.Services.Abstractions;
using MiniMvcProject.Application.UI.ViewModels;
using MiniMvcProject.Application.ViewModels.BasketItemViewModels;
using MiniMvcProject.Application.ViewModels.SubscriptionViewModels;
using System.Diagnostics;

namespace MiniMvcProject.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class HomeController : Controller
    {
        private readonly ISubscriptionService _subscriptionService;
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IBasketService _basketService;
        private readonly IBasketItemService _basketItemService;
        private readonly IMapper _mapper;
        public HomeController(ICategoryService categoryService, IProductService productService, IBasketService basketService, IBasketItemService basketItemService, IMapper mapper, ISubscriptionService subscriptionService)
        {
            _categoryService = categoryService;
            _productService = productService;
            _basketService = basketService;
            _basketItemService = basketItemService;
            _mapper = mapper;
            _subscriptionService = subscriptionService;
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
            if (HttpContext.User.Identity!.IsAuthenticated)
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

        [HttpPost]
        public async Task<IActionResult> SubscribeTo(SubscriptionCreateViewModel vm)
        {
            if (!ModelState.IsValid)
                return RedirectToAction(nameof(Index));

            await _subscriptionService.CreateAsync(vm);

            return RedirectToAction(nameof(Index));
        }
    }
}
