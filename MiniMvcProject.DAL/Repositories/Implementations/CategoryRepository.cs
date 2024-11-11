using MiniMvcProject.DAL.DataContext;
using MiniMvcProject.Domain.Entities;
using MiniMvcProject.Persistance.Repositories.Abstractions;
using MiniMvcProject.Persistance.Repositories.Implementations.Generic;

namespace MiniMvcProject.Persistance.Repositories.Implementations
{

    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }
    }
}
