using MiniMvcProject.Application.ViewModels.CategoryViewModels;
using MiniMvcProject.Application.ViewModels.ProductViewModels;
using MiniMvcProject.Application.ViewModels.ServiceViewModels;
using MiniMvcProject.Application.ViewModels.SliderViewModels;

namespace MiniMvcProject.Application.UI.ViewModels
{
    public class HomeViewModel
    {
        public List<CategoryViewModel> TopThreeCategories { get; set; } = new();
        public List<SliderViewModel> Sliders { get; set; } = new();
        public List<ServiceViewModel> Services { get; set; } = new();
        public List<ProductViewModel> Products { get; set; } = new();

    }
}
