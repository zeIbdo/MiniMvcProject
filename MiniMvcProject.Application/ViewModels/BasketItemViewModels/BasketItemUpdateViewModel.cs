using MiniMvcProject.Application.ViewModels.Generic;

namespace MiniMvcProject.Application.ViewModels.BasketItemViewModels
{
    public class BasketItemUpdateViewModel : IViewModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string? AppUserName { get; set; }
        public int Count { get; set; }
    }
}
