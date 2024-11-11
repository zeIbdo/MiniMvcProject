using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniMvcProject.Domain.Entities;

namespace MiniMvcProject.Persistance.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.RewardPoints).IsRequired();
            builder.Property(x => x.CategoryId).IsRequired();
            builder.Property(x => x.Rating).IsRequired();
            builder.Property(x => x.StockAmount).IsRequired();
            builder.Property(x => x.Code).IsRequired().HasMaxLength(256);
            builder.Property(x => x.Brand).IsRequired().HasMaxLength(256);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(256);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(256);
            builder.Property(x => x.MainPrice).HasColumnType("decimal(18,2)").HasDefaultValue(0);
            builder.Property(x => x.DiscountPrice).HasColumnType("decimal(18,2)").HasDefaultValue(0);

        }
    }
}
