namespace MiniMvcProject.Domain.Entities
{
    public class ProductImage:BaseEntity
    {
        public int ProductId { get; set; }
        public required string ImageUrl { get; set; }
        public bool IsMain { get; set; } = false;
        public bool IsSecondary { get; set; }=false;
        public Product Product { get; set; } = null!;

    }
}
