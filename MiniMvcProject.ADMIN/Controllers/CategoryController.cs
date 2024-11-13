using Microsoft.AspNetCore.Mvc;
using MiniMvcProject.Application.Services.Abstractions;
using MiniMvcProject.ADMIN.FilterAttributes;
using MiniMvcProject.Application.ViewModels.CategoryViewModels;

namespace MiniMvcProject.ADMIN.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetListAsync();
            return View(categories.Data );
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateViewModel vm)
        {
            if(!ModelState.IsValid)
                return View(vm);

            var result = await _categoryService.CreateAsync(vm);
            if (!result.Success)
            {
                ModelState.AddModelError("", result.Message);
                return View(vm);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var result =await _categoryService.GetUpdateViewModel(x => x.Id == id);

            if (!result.Success)            
                return NotFound();

            return View(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CategoryUpdateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                var newVm = await _categoryService.GetUpdateViewModel(x=>x.Id==vm.Id);
                return View(newVm);
            }

            var result = await _categoryService.UpdateAsync(vm);

            if (!result.Success)
            {
                ModelState.AddModelError("", result.Message);
                return View(vm);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _categoryService.RemoveAsync(id);

            if(!result.Success)
                return NotFound(result.Message);
            return RedirectToAction(nameof(Index));
        }
    }
}
