namespace MiniMvcProject.Application.ViewModels.CategoryViewModels
{
    public class CategoryCreateViewModel
    {
        public required string Name { get; set; }
        public int? ParentId { get; set; }
    }
}
