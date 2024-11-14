using Microsoft.AspNetCore.Http;
using MiniMvcProject.Application.ViewModels.Generic;

namespace MiniMvcProject.Application.ViewModels.SliderViewModels
{
    public class SliderCreateViewModel : IViewModel
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required IFormFile Image { get; set; }
        public string? ImageUrl { get; set; }
        public decimal Price { get; set; }
    }
}
