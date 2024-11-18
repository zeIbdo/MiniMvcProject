using MiniMvcProject.Application.ViewModels.ProductViewModels;

namespace MiniMvcProject.Application.UI.ViewModels
{
    public class PagedViewModel
    {
        public List<ProductViewModel> Products { get; set; } = new();
        public int TotalPages { get; set; }


    }
}
