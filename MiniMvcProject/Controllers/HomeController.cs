using Microsoft.AspNetCore.Mvc;
using MiniMvcProject.Application.Services.Abstractions;
using MiniMvcProject.Application.UI.ViewModels;
using System.Diagnostics;

namespace MiniMvcProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        public HomeController(ICategoryService categoryService, IProductService productService)
        {
            _categoryService = categoryService;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var resultCategories = await _categoryService.GetListAsync(predicate: x => x.SubCategories.Count == 0 && x.ParentCategoryId == null, index: 0, size: 3);
            var homeVm = new HomeViewModel
            {
                TopThreeCategories = resultCategories.Data!.ToList(),
            };
            return View(homeVm);
        }
        public async Task<IActionResult> RelatedProducts(int categoryId)
        {
            var productsResult = await _productService.GetListAsync(predicate:x=>x.CategoryId==categoryId);

            return PartialView(productsResult.Data!.ToList());
        }

    }
}
