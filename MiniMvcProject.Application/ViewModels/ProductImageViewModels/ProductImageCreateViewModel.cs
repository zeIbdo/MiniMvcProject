using MiniMvcProject.Application.ViewModels.Generic;

namespace MiniMvcProject.Application.ViewModels.ProductImageViewModels
{
    public class ProductImageCreateViewModel : IViewModel
    {
        public required int ProductId { get; set; }
        public required string ImageUrl { get; set; }
        public bool IsMain { get; set; } = false;
        public bool IsSecondary { get; set; } = false;
    }
}
