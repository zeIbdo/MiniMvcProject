using MiniMvcProject.Application.ViewModels.Generic;

namespace MiniMvcProject.Application.ViewModels.BasketItemViewModels
{
    public class BasketItemCreateViewModel : IViewModel
    {
        public int ProductId { get; set; }
        public required string AppUserId { get; set; }
        public int Count { get; set; }
    }
}
