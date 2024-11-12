using MiniMvcProject.Application.ViewModels.Generic;

namespace MiniMvcProject.Application.ViewModels.CategoryViewModels
{
    public class CategoryViewModel:IViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? ParentId { get; set; }
        public List<CategoryViewModel> SubCategories { get; set; } = new();
        public bool HasSubCategories => SubCategories.Any();
    }
}
