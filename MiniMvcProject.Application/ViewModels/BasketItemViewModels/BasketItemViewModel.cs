using MiniMvcProject.Application.ViewModels.Generic;
using MiniMvcProject.Application.ViewModels.ProductViewModels;
using MiniMvcProject.Domain.Entities;

namespace MiniMvcProject.Application.ViewModels.BasketItemViewModels
{
    public class BasketItemViewModel:IViewModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string? AppUserId { get; set; }
        public int Count { get; set; }
    }

}
