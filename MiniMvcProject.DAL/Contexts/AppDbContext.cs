using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MiniMvcProject.Domain.Entities;

namespace MiniMvcProject.DAL.DataContext;

public class AppDbContext:IdentityDbContext<AppUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder builder)
    {

        base.OnModelCreating(builder);

        builder.Entity<Product>()
            .HasQueryFilter(p => !p.IsDeleted);

        builder.Entity<Product>().ToTable(x=>x.HasCheckConstraint("CK_Product_Rating", "[Rating] <= 5"));
    }
}
