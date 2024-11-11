using MiniMvcProject.Application.ViewModels.Generic;

namespace MiniMvcProject.Application.ViewModels.ProductImageViewModels
{
    public class ProductImageViewModel : IViewModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsMain { get; set; }
        public bool IsSecondary { get; set; }
    }
}
