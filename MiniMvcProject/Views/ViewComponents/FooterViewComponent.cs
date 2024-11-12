using Microsoft.AspNetCore.Mvc;
using MiniMvcProject.Application.Services.Abstractions;
using MiniMvcProject.Application.UI.ViewModels;

namespace MiniMvcProject.MVC.Views.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        private readonly ISettingService _settingService;

        public FooterViewComponent(ISettingService settingService)
        {
            _settingService = settingService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var resultAddress = await _settingService.GetAsync(x => x.Key.ToLower() == "address");
            var resultPhone = await _settingService.GetAsync(x => x.Key.ToLower() == "phone");
            var resultEmail = await _settingService.GetAsync(x => x.Key.ToLower() == "email");
            var resultFacebook = await _settingService.GetAsync(x => x.Key.ToLower() == "facebook");
            var resultYoutube = await _settingService.GetAsync(x => x.Key.ToLower() == "youtube");
            var resultGoogle = await _settingService.GetAsync(x => x.Key.ToLower() == "google");
            var resultTwitter = await _settingService.GetAsync(x => x.Key.ToLower() == "twitter");
            var resultBottomText = await _settingService.GetAsync(x => x.Key.ToLower() == "bottomtext");
            var footer = new FooterViewModel()
            {
                Address = resultAddress.Data!.Value,
                Phone = resultPhone.Data!.Value,
                Email = resultEmail.Data!.Value,
                FacebookLink = resultFacebook.Data!.Value,
                YouTubeLink = resultYoutube.Data!.Value,
                TwitterLink = resultGoogle.Data!.Value,
                GoogleLink = resultTwitter.Data!.Value,
                BottomText= resultBottomText.Data!.Value,
            };
            return View(footer);
        }
    }
}
