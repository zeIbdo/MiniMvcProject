using AutoMapper;
using MiniMvcProject.Application.Services.Abstractions;
using MiniMvcProject.Application.Services.Implementations.Generic;
using MiniMvcProject.Application.ViewModels.ServiceViewModels;
using MiniMvcProject.Domain.Entities;
using MiniMvcProject.Persistance.Repositories.Abstractions.Generic;

namespace MiniMvcProject.Application.Services.Implementations
{
    public class ServiceManager : CrudManager<Service, ServiceViewModel, ServiceCreateViewModel, ServiceUpdateViewModel>, IServiceService
    {
        public ServiceManager(IRepository<Service> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }


}
