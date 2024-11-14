using Microsoft.AspNetCore.Mvc.Rendering;
using MiniMvcProject.Application.ViewModels.Generic;

namespace MiniMvcProject.Application.ViewModels.ProductTagViewModels
{
    public class ProductTagCreateViewModel : IViewModel
    {
        public int ProductId { get; set; }
        public int TagId { get; set; }
    }
}
