using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniMvcProject.Domain.Entities;

namespace MiniMvcProject.Persistance.Configurations
{
    public class SliderConfiguration : IEntityTypeConfiguration<Slider>
    {
        public void Configure(EntityTypeBuilder<Slider> builder)
        {
            builder.Property(x => x.ImageUrl).IsRequired().HasMaxLength(2048);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(256);
            builder.Property(x => x.Title).IsRequired().HasMaxLength(256);
        }
    }
}
