namespace MiniMvcProject.Application.ViewModels.ProductViewModels
{
    public class ProductCreateViewModel
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

    }
}
