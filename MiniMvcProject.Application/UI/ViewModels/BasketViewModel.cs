using MiniMvcProject.Application.ViewModels.ProductViewModels;
using MiniMvcProject.Domain.Entities;

namespace MiniMvcProject.Application.UI.ViewModels
{
    public class BasketViewModel
    {
        public ProductViewModel Product { get; set; } = null!;
        public int Count { get; set; }
    }
}
