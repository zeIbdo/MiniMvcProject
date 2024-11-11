using Microsoft.EntityFrameworkCore.Query;
using MiniMvcProject.Application.ViewModels.Generic;
using MiniMvcProject.Domain.Entities;
using System.Linq.Expressions;

namespace MiniMvcProject.Application.Services.Abstractions
{
    public interface ICrudService<T,TVm,TCrVm,TUpVm>
        where T : BaseEntity
        where TVm : IViewModel
        where TCrVm : IViewModel
        where TUpVm : IViewModel
    {
        Task<TVm?> GetAsync(int id);

        Task<TVm?> GetAsync(Expression<Func<T, bool>> predicate,
                                   Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
                                   int index = 0, int size = 10, bool enableTracking = true);
        Task<IEnumerable<TVm>> GetListAsync(Expression<Func<T, bool>>? predicate = null,
                                         Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
                                         Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null);
        Task<TVm> CreateAsync(TCrVm createViewModel);
        Task<TVm> UpdateAsync(TUpVm entity);
        Task<TVm> RemoveAsync(int id);
    }
}
