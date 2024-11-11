using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniMvcProject.Domain.Entities;

namespace MiniMvcProject.Persistance.Configurations
{
    public class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.Property(x => x.IconUrl).IsRequired().HasMaxLength(2048);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(256);
            builder.Property(x => x.Title).IsRequired().HasMaxLength(256);
        }
    }
}
