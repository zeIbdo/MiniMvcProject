using AutoMapper;
using MiniMvcProject.Application.Services.Abstractions;
using MiniMvcProject.Application.Services.Implementations.Generic;
using MiniMvcProject.Application.ViewModels.SettingViewModels;
using MiniMvcProject.Domain.Entities;
using MiniMvcProject.Persistance.Repositories.Abstractions.Generic;

namespace MiniMvcProject.Application.Services.Implementations
{
    public class SettingManager : CrudManager<Setting, SettingViewModel, SettingCreateViewModel, SettingUpdateViewModel>, ISettingService
    {
        public SettingManager(IRepository<Setting> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }


}
