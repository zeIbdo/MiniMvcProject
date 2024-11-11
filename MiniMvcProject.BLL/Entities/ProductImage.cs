namespace MiniMvcProject.Domain.Entities
{
    public class ProductImage:BaseEntity
    {
        public int ProductId { get; set; }
        public required string ImageUrl { get; set; }
        public bool IsMain { get; set; }
        public bool IsSecondary { get; set; }
        public Product Product { get; set; } = null!;
        public List<ProductTag> ProductTags { get; set; } = new();

    }
}
