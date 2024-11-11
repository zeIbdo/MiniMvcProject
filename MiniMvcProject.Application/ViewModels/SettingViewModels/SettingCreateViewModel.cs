using MiniMvcProject.Application.ViewModels.Generic;

namespace MiniMvcProject.Application.ViewModels.SettingViewModels
{
    public class SettingCreateViewModel : IViewModel
    {
        public required string Key { get; set; }
        public required string Value { get; set; }
    }
}
