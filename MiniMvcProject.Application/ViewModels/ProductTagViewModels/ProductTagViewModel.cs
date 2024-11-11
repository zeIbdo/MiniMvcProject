using MiniMvcProject.Application.ViewModels.Generic;
using MiniMvcProject.Application.ViewModels.ProductViewModels;
using MiniMvcProject.Application.ViewModels.TagViewModels;

namespace MiniMvcProject.Application.ViewModels.ProductTagViewModels
{
    public class ProductTagViewModel:IViewModel
    {
        public int Id { get; set; }
        public ProductViewModel? Product { get; set; }
        public TagViewModel? Tag { get; set; }
    }
}
