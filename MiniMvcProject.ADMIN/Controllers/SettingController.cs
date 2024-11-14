using Microsoft.AspNetCore.Mvc;
using MiniMvcProject.Application.Services.Abstractions;
using MiniMvcProject.Application.ViewModels.SettingViewModels;
using MiniMvcProject.Application.ViewModels.TagViewModels;

namespace MiniMvcProject.ADMIN.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class SettingController : Controller
    {
        private readonly ISettingService _settingService;

        public SettingController(ISettingService settingService)
        {
            _settingService = settingService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _settingService.GetListAsync();

            return View(result.Data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SettingCreateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            await _settingService.CreateAsync(vm);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            var result = await _settingService.GetUpdateViewModel(x => x.Id == id);

            if (!result.Success)
                return NotFound();

            return View(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Update(SettingUpdateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                var newVm = await _settingService.GetUpdateViewModel(x => x.Id == vm.Id);
                return View(newVm);
            }

            await _settingService.UpdateAsync(vm);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _settingService.RemoveAsync(id);

            if (!result.Success)
                return NotFound(result.Message);

            return RedirectToAction(nameof(Index));
        }
    }
}
