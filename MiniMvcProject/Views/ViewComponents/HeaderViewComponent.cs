using Microsoft.AspNetCore.Mvc;
using MiniMvcProject.Application.Services.Abstractions;
using MiniMvcProject.Application.UI.ViewModels;

namespace MiniMvcProject.MVC.Views.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly ISettingService _settingService;
        private readonly ICategoryService _categoryService;

        public HeaderViewComponent(ISettingService settingService, ICategoryService categoryService)
        {
            _settingService = settingService;
            _categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var resultSupportNumber = await _settingService.GetAsync(x => x.Key.ToLower() == "supportnumber");
            var resultCategories = await _categoryService.GetListAsync();
            var headerViewModel = new HeaderViewModel()
            {
                SupportNumber = resultSupportNumber.Data!.Value,
                Categories = resultCategories.Data!.ToList()
            };
            return View(headerViewModel);
        }

    }
}
