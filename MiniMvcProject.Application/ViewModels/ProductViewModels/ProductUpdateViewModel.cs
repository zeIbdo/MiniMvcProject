using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using MiniMvcProject.Application.ViewModels.Generic;

namespace MiniMvcProject.Application.ViewModels.ProductViewModels
{
    public class ProductUpdateViewModel:IViewModel
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
        public int CategoryId { get; set; }
        public List<IFormFile>? Images { get; set; }
        public List<int>? OldTagIds { get; set; }
        public List<SelectListItem>? OldTags { get; set; }
        public List<int>? NewTagIds { get; set; }
        public List<SelectListItem>? NewTags { get; set; }

    }
}
