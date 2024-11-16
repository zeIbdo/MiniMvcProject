using MiniMvcProject.Application.ViewModels.BasketItemViewModels;
using MiniMvcProject.Application.ViewModels.CategoryViewModels;
using MiniMvcProject.Application.ViewModels.Generic;
using MiniMvcProject.Application.ViewModels.ProductImageViewModels;
using MiniMvcProject.Application.ViewModels.TagViewModels;

namespace MiniMvcProject.Application.ViewModels.ProductViewModels
{
    public class ProductViewModel:IViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal MainPrice { get; set; }
        public decimal DiscountPrice { get; set; }
        public bool InStock { get; set; }
        public string? Brand { get; set; }
        public string? Code { get; set; }
        public int RewardPoints { get; set; }
        public int StockAmount { get; set; }
        public int Rating { get; set; }
        public bool IsDeleted { get; set; }
        public int ViewCount { get; set; }
        public List<ProductImageViewModel>? Images { get; set; }
        public CategoryViewModel? Category { get; set; }
        public List<TagViewModel>? Tags { get; set; }
        public List<BasketItemViewModel>? BasketItems { get; set; }

    }
}
