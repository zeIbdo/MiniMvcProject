using Microsoft.AspNetCore.Mvc;
using MiniMvcProject.Application.Services.Abstractions;

namespace MiniMvcProject.MVC.Controllers
{
    public class ShopController : Controller
    {
        private readonly IProductService _productService;

        public ShopController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index(int index=0, int size = 1)
        {
            var products = await _productService.GetPaginatedProductsAsync(index:index,size:size);
            return View(products);
        }
    }
}
