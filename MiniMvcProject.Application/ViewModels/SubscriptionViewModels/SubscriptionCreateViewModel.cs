using MiniMvcProject.Application.ViewModels.Generic;

namespace MiniMvcProject.Application.ViewModels.SubscriptionViewModels
{
    public class SubscriptionCreateViewModel : IViewModel
    {
        public required string Email { get; set; }
    }
}
