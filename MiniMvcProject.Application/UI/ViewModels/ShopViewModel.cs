using core.Persistance.Paging;
using MiniMvcProject.Application.ViewModels.CategoryViewModels;
using MiniMvcProject.Application.ViewModels.ProductViewModels;
using MiniMvcProject.Domain.Enums;

namespace MiniMvcProject.Application.UI.ViewModels
{
    public class ShopViewModel
    {
        public int Index { get; set; } = 0; 
        public int Size { get; set; } = 1; 
        public int? CategoryId { get; set; }
        public List<CategoryViewModel> Categories { get; set; } = new();
        public Paginate<ProductViewModel> PaginatedProducts { get; set; }= new();
        public SortTypes? SortType { get; set; } = null;
    }
}
