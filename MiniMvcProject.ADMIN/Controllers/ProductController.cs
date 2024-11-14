using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniMvcProject.Application.Services.Abstractions;
using MiniMvcProject.Application.ViewModels.ProductViewModels;

namespace MiniMvcProject.ADMIN.Controllers
{
    [AutoValidateAntiforgeryToken]
    [Authorize(Roles = "Admin,Moderator")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _productService.GetListAsync(include:x=>x.Include(x=>x.Category).Include(x => x.ProductImages));

            return View(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var vm = await _productService.GetProductCreateViewModelAsync(new ProductCreateViewModel { Brand = null!, Code = null!, Description = null!, Name=null!,AdditionalImages=null!,HoverImage=null!,MainImage=null! });
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateViewModel model)
        {
            if(!ModelState.IsValid)
            {   
                var newVm = await _productService.GetProductCreateViewModelAsync(model);
                return View(newVm);
            }

            var result = await _productService.CreateAsync(model);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var result = await _productService.GetProductUpdateViewModelAsync(id);

            if(result.Message.ToLower()=="not found")
                return NotFound();

            return View(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductUpdateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                var newVm = (await _productService.GetProductUpdateViewModelAsync(vm.Id)).Data;
                return View(newVm);
            }
            var result = await _productService.UpdateAsync(vm);
            if (result.Success == false)
            {
                ModelState.AddModelError("", result.Message);
                var newVm = (await _productService.GetProductUpdateViewModelAsync(vm.Id)).Data;
                return View(newVm);
            }
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _productService.SoftDeleteProductAsync(id);

            if (!result.Success)
                return NotFound(result.Message);

            return RedirectToAction(nameof(Index));
        }
    }
}
