namespace MiniMvcProject.Domain.Entities
{
    public class Category:BaseEntity
    {
        public required string Name { get; set; }
        public int? ParentCategoryId { get; set; }
        public Category? ParentCategory { get; set; }
        public List<Category> SubCategories { get; set; } = new();
        public List<Product> Products { get; set; } = new();
    }
}
