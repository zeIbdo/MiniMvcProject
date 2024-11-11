using MiniMvcProject.DAL.DataContext;
using MiniMvcProject.Domain.Entities;
using MiniMvcProject.Persistance.Repositories.Abstractions;
using MiniMvcProject.Persistance.Repositories.Implementations.Generic;

namespace MiniMvcProject.Persistance.Repositories.Implementations
{
    public class ServiceRepository : Repository<Service>, IServiceRepository
    {
        public ServiceRepository(AppDbContext context) : base(context)
        {
        }
    }
}
