using MiniMvcProject.Application.ViewModels.ProductViewModels;

namespace MiniMvcProject.Application.UI.ViewModels
{
    public class ProductsViewModel
    {
        public List<ProductViewModel> SelectedProducts { get; set; } = new();
        public int SelectedCategoryId { get; set; }
    }
}
