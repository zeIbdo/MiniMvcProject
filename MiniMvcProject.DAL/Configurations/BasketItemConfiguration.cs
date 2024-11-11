using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniMvcProject.Domain.Entities;

namespace MiniMvcProject.Persistance.Configurations
{
    public class BasketItemConfiguration : IEntityTypeConfiguration<BasketItem>
    {
        public void Configure(EntityTypeBuilder<BasketItem> builder)
        {
            builder.Property(x => x.Count).IsRequired();
            builder.Property(x => x.ProductId).IsRequired();
            builder.Property(x => x.AppUserId).IsRequired();
        }
    }
}
