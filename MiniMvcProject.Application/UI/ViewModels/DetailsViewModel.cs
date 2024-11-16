using MiniMvcProject.Application.ViewModels.ProductViewModels;

namespace MiniMvcProject.Application.UI.ViewModels
{
    public class DetailsViewModel
    {
        public ProductViewModel Product { get; set; } = null!;
        public List<ProductViewModel> RelatedProducts { get; set; } = new();

    }
}
