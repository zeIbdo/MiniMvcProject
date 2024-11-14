using Microsoft.AspNetCore.Mvc;
using MiniMvcProject.Application.Services.Abstractions;
using MiniMvcProject.Application.ViewModels.SliderViewModels;

namespace MiniMvcProject.ADMIN.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class SliderController : Controller
    {
        private readonly ISliderService _sliderService;

        public SliderController(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }

        public async Task<IActionResult> Index()
        {
            var sliders = await _sliderService.GetListAsync();
            return View(sliders.Data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(SliderCreateViewModel vm)
        {
            if(!ModelState.IsValid) 
                return View(vm);
            var result = await _sliderService.CreateAsync(vm);
            if (!result.Success)
            {
                ModelState.AddModelError("",result.Message);
                return View(vm);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            var result  = await _sliderService.GetUpdateViewModel(x=>x.Id == id);

            if (!result.Success)
                return NotFound();

            return View(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Update(SliderUpdateViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var result = await _sliderService.UpdateAsync(vm);
            if (!result.Success)
            {
                ModelState.AddModelError("", result.Message);
                return View(vm);
            }
             return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _sliderService.RemoveAsync(id);

            if (!result.Success)
                return NotFound(result.Message);

            return RedirectToAction(nameof(Index));
        }
    }
}
