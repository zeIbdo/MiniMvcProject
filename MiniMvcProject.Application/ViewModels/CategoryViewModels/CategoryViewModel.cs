namespace MiniMvcProject.Application.ViewModels.CategoryViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? ParentId { get; set; }
    }
}
