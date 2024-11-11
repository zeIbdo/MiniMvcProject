namespace MiniMvcProject.Domain.Entities
{
    public class Category:BaseEntity
    {
        public required string Name { get; set; }
        public int? ParentId { get; set; }
        public List<Product> Products { get; set; } = new();
    }
}
