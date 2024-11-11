using MiniMvcProject.Application.Services.Abstractions.Generic;
using MiniMvcProject.Application.ViewModels.ServiceViewModels;
using MiniMvcProject.Domain.Entities;

namespace MiniMvcProject.Application.Services.Abstractions
{
    public interface IServiceService : ICrudService<Service, ServiceViewModel, ServiceCreateViewModel, ServiceUpdateViewModel>
    {
    }
}
