using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniMvcProject.Application.Services.Abstractions;
using MiniMvcProject.Application.ViewModels.ServiceViewModels;
using MiniMvcProject.Application.ViewModels.SliderViewModels;

namespace MiniMvcProject.ADMIN.Controllers
{
    [AutoValidateAntiforgeryToken]
    [Authorize(Roles = "Admin,Moderator")]
    public class ServiceController : Controller
    {
        private readonly IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _serviceService.GetListAsync();

            return View(result.Data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ServiceCreateViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);
            var result = await _serviceService.CreateAsync(vm);
            if (!result.Success)
            {
                ModelState.AddModelError("", result.Message);
                return View(vm);
            }

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Update(int id)
        {
            var result = await _serviceService.GetUpdateViewModel(x => x.Id == id);

            if (!result.Success)
                return NotFound();

            return View(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ServiceUpdateViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var result = await _serviceService.UpdateAsync(vm);
            if (!result.Success)
            {
                ModelState.AddModelError("", result.Message);
                return View(vm);
            }
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _serviceService.RemoveAsync(id);

            if (!result.Success)
                return NotFound(result.Message);

            return RedirectToAction(nameof(Index));

        }
    }
}
