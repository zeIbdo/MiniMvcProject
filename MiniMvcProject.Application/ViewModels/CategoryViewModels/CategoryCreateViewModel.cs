using MiniMvcProject.Application.ViewModels.Generic;

namespace MiniMvcProject.Application.ViewModels.CategoryViewModels
{
    public class CategoryCreateViewModel:IViewModel
    {
        public required string Name { get; set; }
        public int? ParentId { get; set; }
    }
}
