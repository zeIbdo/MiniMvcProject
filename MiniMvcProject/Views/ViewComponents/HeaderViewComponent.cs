using Microsoft.AspNetCore.Mvc;
using MiniMvcProject.Application.Services.Abstractions;
using MiniMvcProject.Application.UI.ViewModels;

namespace MiniMvcProject.MVC.Views.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly ISettingService _settingService;
        private readonly ICategoryService _categoryService;
        private readonly IBasketService _basketService;

        public HeaderViewComponent(ISettingService settingService, ICategoryService categoryService, IBasketService basketService)
        {
            _settingService = settingService;
            _categoryService = categoryService;
            _basketService = basketService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var resultSupportNumber = await _settingService.GetAsync(x => x.Key.ToLower() == "supportnumber");
            var resultCategories = await _categoryService.GetListAsync();
            var headerViewModel = new HeaderViewModel()
            {
                SupportNumber = resultSupportNumber.Data!.Value,
                Categories = resultCategories.Data!.ToList(),
            };
            return View(headerViewModel);
        }

    }
}
