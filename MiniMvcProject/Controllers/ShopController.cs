using core.Persistance.Paging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniMvcProject.Application.Services.Abstractions;
using MiniMvcProject.Application.UI.ViewModels;
using MiniMvcProject.Application.ViewModels.ProductViewModels;
using MiniMvcProject.Domain.Enums;

namespace MiniMvcProject.MVC.Controllers
{
    public class ShopController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ShopController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index(int index = 0, int size = 1, int? categoryId = null,SortTypes? sortBy=null)
        {
            var products = new Paginate<ProductViewModel>();
            if (categoryId == null)
                products = await _productService.GetPaginatedProductsAsync(index: index, size: size);
            else
                products = await _productService.GetPaginatedProductsAsync(index: index, size: size,include:x=>x.Include(x=>x.Category).ThenInclude(x=>x.ParentCategory), predicate: x => x.CategoryId == categoryId||x.Category.ParentCategory.Id==categoryId);

            if (sortBy is not null)
            {
                switch(sortBy)
                {
                    case SortTypes.AZ:
                        products.Items=products.Items.OrderBy(x => x.Name).ToList();
                        break;
                    case SortTypes.ZA:
                        products.Items = products.Items.OrderByDescending(x => x.Name).ToList();
                        break;
                    case SortTypes.PRICEDESCENDING:
                        products.Items = products.Items.OrderByDescending(x => x.MainPrice).ToList();
                        break;
                    case SortTypes.PRICEASCENDING:
                        products.Items = products.Items.OrderBy(x => x.MainPrice).ToList();
                        break;
                    case SortTypes.RATINGASCENDING:
                        products.Items = products.Items.OrderBy(x => x.Rating).ToList();
                        break;
                    case SortTypes.RATINGDESCENDING:
                        products.Items = products.Items.OrderByDescending(x => x.Rating).ToList();
                        break;
                }
            }
            var categories = await _categoryService.GetListAsync(include: s => s.Include(x => x.SubCategories), enableTracking: false);
            var vm = new ShopViewModel
            {
                Categories = categories.Data!.ToList(),
                PaginatedProducts = products,
                Size = size,
                Index = index,
                CategoryId = categoryId,

            };
            if(sortBy!=null)
                vm.SortType = sortBy;
           
            return View(vm);
        }
    }
}
