using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MiniMvcProject.Domain.Entities;
using System.Reflection.Emit;

namespace MiniMvcProject.DAL.DataContext;

public class AppDbContext:IdentityDbContext<AppUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<BasketItem> BasketItems { get; set; }
    public DbSet<ProductImage> ProductImages { get; set; }
    public DbSet<ProductTag> ProductTags { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<Setting> Settings { get; set; }
    public DbSet<Slider> Sliders { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<Tag> Tags { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<BasketItem>()
        .HasOne(b => b.Product)
        .WithMany(p => p.BasketItems)
        .HasForeignKey(b => b.ProductId);

        builder.Entity<Product>()
            .HasQueryFilter(p => !p.IsDeleted);

        builder.Entity<Product>().ToTable(x=>x.HasCheckConstraint("CK_Product_Rating", "[Rating] <= 5"));
    }
}
