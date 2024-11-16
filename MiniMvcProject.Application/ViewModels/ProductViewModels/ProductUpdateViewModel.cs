using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using MiniMvcProject.Application.ViewModels.Generic;

namespace MiniMvcProject.Application.ViewModels.ProductViewModels
{
    public class ProductUpdateViewModel:IViewModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public decimal MainPrice { get; set; }
        public decimal DiscountPrice { get; set; }
        public int ViewCount { get; set; }
        public bool InStock { get; set; }
        public required string Brand { get; set; }
        public required string Code { get; set; }
        public int RewardPoints { get; set; }
        public int StockAmount { get; set; }
        public int Rating { get; set; }
        public bool IsDeleted { get; set; }
        public int CategoryId { get; set; }
        public List<SelectListItem> Categories { get; set; } = new();
        public  IFormFile? MainImage { get; set; }
        public string? MainImageUrl { get; set; }
        public string? SecondaryImageUrl { get; set; }
        public List<string> AdditonalImageUrls { get; set; } = new List<string>();
        public List<int> AdditonalImageIds { get; set; } = new List<int>();
        public  IFormFile? HoverImage { get; set; }
        public  List<IFormFile>? AdditionalImages { get; set; }
        public List<int>? OldTagIds { get; set; }
        public List<SelectListItem>? OldTags { get; set; }
        public List<int>? NewTagIds { get; set; }
        public List<SelectListItem>? NewTags { get; set; }
        public string? ImagesToDelete { get; set; }
    }
}
