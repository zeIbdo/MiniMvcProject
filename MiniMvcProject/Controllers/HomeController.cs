using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniMvcProject.Application.Services.Abstractions;
using MiniMvcProject.Application.UI.ViewModels;
using MiniMvcProject.Application.Utilities;
using MiniMvcProject.Application.ViewModels.BasketItemViewModels;
using MiniMvcProject.Application.ViewModels.SubscriptionViewModels;
using Newtonsoft.Json;
using System.Diagnostics;

namespace MiniMvcProject.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class HomeController : Controller
    {
        private readonly IEmailService _emailService;
        private readonly ISliderService _sliderService;
        private readonly IServiceService _serviceService;
        private readonly ISubscriptionService _subscriptionService;
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IBasketService _basketService;
        private readonly IBasketItemService _basketItemService;
        private readonly IMapper _mapper;
        public HomeController(ICategoryService categoryService, IProductService productService, IBasketService basketService, IBasketItemService basketItemService, IMapper mapper, ISubscriptionService subscriptionService, IEmailService emailService, ISliderService sliderService, IServiceService serviceService)
        {
            _categoryService = categoryService;
            _productService = productService;
            _basketService = basketService;
            _basketItemService = basketItemService;
            _mapper = mapper;
            _subscriptionService = subscriptionService;
            _emailService = emailService;
            _sliderService = sliderService;
            _serviceService = serviceService;
        }

        public async Task<IActionResult> Index()
        {
            var resultCategories = await _categoryService.GetListAsync(predicate: x => x.SubCategories.Count == 0 && x.ParentCategoryId == null, index: 0, size: 3);
            var sliders = await _sliderService.GetListAsync(enableTracking: false);
            var services = await _serviceService.GetListAsync(enableTracking: false);
            var products = await _productService.GetListAsync(include: x => x.Include(x => x.ProductImages).Include(x => x.ProductImages).Include(x => x.BasketItems), orderBy: x => x.OrderByDescending(x => x.BasketItems.Sum(x => x.Count)), enableTracking: false);
            return View(new HomeViewModel { TopThreeCategories = resultCategories.Data!.ToList(), Services = services.Data!.ToList(), Sliders = sliders.Data!.ToList(), Products = products.Data!.ToList() });
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
            var basketItems = new List<BasketItemViewModel>();

            if (HttpContext.User.Identity.IsAuthenticated)
                basketItems = await _basketItemService.GetBasketAsync();

            else
                basketItems = _basketItemService.GetBasketCookies();
                return PartialView("_BasketDbPartial", basketItems);

        }

        [HttpPost]
        public async Task<IActionResult> SubscribeTo(SubscriptionCreateViewModel vm)
        {
            string? returnUrl = Request.Headers["Referer"];
            if (string.IsNullOrEmpty(returnUrl))
                returnUrl = "/";
            if (!ModelState.IsValid)
                return Redirect(returnUrl);

            await _subscriptionService.CreateAsync(vm);

            return Redirect(returnUrl);
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel vm)
        {
            _emailService.SendEmail("ackermanlevi2005@gmail.com", vm.Message, $"From : Name - {vm.Name} Email - {vm.Email}");

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _productService.GetAsync(x => x.Id == id, x => x.Include(x => x.ProductImages).Include(x => x.ProductTags).ThenInclude(x => x.Tag), enableTracking: false);

            if (product.Data == null)
                return NotFound();
            await _productService.IncrementViewCountAsync(id);

            return View(product.Data);
        }

        public async Task<IActionResult> Cart()
        {
            var basketItems = new List<BasketItemViewModel>();
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                basketItems = await _basketItemService.GetBasketAsync();
            }
            else
            {
                basketItems = _basketItemService.GetBasketCookies();
            }
            return View(basketItems);
        }
    }
}
