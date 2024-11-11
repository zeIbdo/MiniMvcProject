using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniMvcProject.Domain.Entities;

namespace MiniMvcProject.Persistance.Configurations
{
    public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.Property(x => x.ProductId).IsRequired();
            builder.Property(x => x.ImageUrl).IsRequired().HasMaxLength(2048);
        }
    }
}
