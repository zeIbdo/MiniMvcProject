using AutoMapper;
using MiniMvcProject.Application.Services.Abstractions;
using MiniMvcProject.Application.Services.Implementations.Generic;
using MiniMvcProject.Application.ViewModels.SubscriptionViewModels;
using MiniMvcProject.Domain.Entities;
using MiniMvcProject.Persistance.Repositories.Abstractions.Generic;

namespace MiniMvcProject.Application.Services.Implementations
{
    public class SubscriptionManager : CrudManager<Subscription, SubscriptionViewModel, SubscriptionCreateViewModel, SubscriptionUpdateViewModel>, ISubscriptionService
    {
        public SubscriptionManager(IRepository<Subscription> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }


}
