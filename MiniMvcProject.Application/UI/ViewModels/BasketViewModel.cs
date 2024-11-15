using MiniMvcProject.Application.ViewModels.ProductViewModels;
using MiniMvcProject.Domain.Entities;

namespace MiniMvcProject.Application.UI.ViewModels
{
    public class BasketViewModel
    {
        public int ProductId { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductName { get; set; } = null!;
        public int Count { get; set; }
    }
}
