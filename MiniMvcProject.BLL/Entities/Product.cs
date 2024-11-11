namespace MiniMvcProject.Domain.Entities
{
    public class Product:BaseEntity
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public decimal MainPrice { get; set; }
        public decimal DiscountPrice { get; set; }
        public bool InStock { get; set; }
        public required string Brand { get; set; }
        public required string Code { get; set; }
        public int RewardPoints { get; set; }
        public int StockAmount { get; set; }
        public int Rating { get; set; }
        public bool IsDeleted { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public List<ProductImage> ProductImages { get; set; } = new();
        public List<ProductTag> ProductTags { get; set; } = new();

    }
}
