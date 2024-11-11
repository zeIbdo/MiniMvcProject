namespace MiniMvcProject.Domain.Entities
{
    public class BasketItem:BaseEntity
    {
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public AppUser AppUser { get; set; } =null!;
        public required string AppUserId { get; set; } 
        public int Count { get; set; }
    }
}
