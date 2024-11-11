using Microsoft.AspNetCore.Mvc.Rendering;
using MiniMvcProject.Application.ViewModels.Generic;

namespace MiniMvcProject.Application.ViewModels.ProductTagViewModels
{
    public class ProductTagCreateViewModel : IViewModel
    {
        public List<SelectListItem>? Products { get; set; }
        public int ProductId { get; set; }
        public List<SelectListItem>? Tags { get; set; }
        public int TagId { get; set; }
    }
}
