using MiniMvcProject.Application.Services.Abstractions.Generic;
using MiniMvcProject.Application.ViewModels.SubscriptionViewModels;
using MiniMvcProject.Domain.Entities;

namespace MiniMvcProject.Application.Services.Abstractions
{
    public interface ISubscriptionService : ICrudService<Subscription, SubscriptionViewModel, SubscriptionCreateViewModel, SubscriptionUpdateViewModel>
    {
    }
}
