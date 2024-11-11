namespace MiniMvcProject.Application.ViewModels.CategoryViewModels
{
    public class CategoryUpdateViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? ParentId { get; set; }
    }
}
