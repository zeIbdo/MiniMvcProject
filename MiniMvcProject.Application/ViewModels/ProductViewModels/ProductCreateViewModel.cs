using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using MiniMvcProject.Application.ViewModels.Generic;

namespace MiniMvcProject.Application.ViewModels.ProductViewModels
{
    public class ProductCreateViewModel:IViewModel
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public decimal MainPrice { get; set; }
        public decimal DiscountPrice { get; set; }
        public bool InStock { get; set; }
        public required string Brand { get; set; }
        public required string Code { get; set; }
        public int RewardPoints { get; set; }
        public int StockAmount { get; set; }
        public int Rating { get; set; }
        public bool IsDeleted { get; set; }
        public List<SelectListItem>? Categories { get; set; }
        public int CategoryId { get; set; }
        //public required IFormFile MainImage { get; set; } 
        //public string? MainImageUrl { get; set; }
        //public string? SecondaryImageUrl { get; set; }
        //public List<string> additonalImageUrls { get; set; } = new List<string>();
        //public required IFormFile HoverImage { get; set; } 
        //public required List<IFormFile> AdditionalImages { get; set; } 
        public List<int>? TagIds { get; set; } 
        public List<SelectListItem>? Tags { get; set; } 

    }
}
