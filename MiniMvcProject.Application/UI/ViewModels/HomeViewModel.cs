using MiniMvcProject.Application.ViewModels.CategoryViewModels;
using MiniMvcProject.Application.ViewModels.ProductViewModels;

namespace MiniMvcProject.Application.UI.ViewModels
{
    public class HomeViewModel
    {
        public List<CategoryViewModel> TopThreeCategories { get; set; } = new();
        public List<ProductViewModel> Products { get; set; } = new();

    }
}
