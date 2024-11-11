using MiniMvcProject.Application.Services.Abstractions.Generic;
using MiniMvcProject.Application.ViewModels.SettingViewModels;
using MiniMvcProject.Domain.Entities;

namespace MiniMvcProject.Application.Services.Abstractions
{
    public interface ISettingService : ICrudService<Setting, SettingViewModel, SettingCreateViewModel, SettingUpdateViewModel>
    {
    }
}
