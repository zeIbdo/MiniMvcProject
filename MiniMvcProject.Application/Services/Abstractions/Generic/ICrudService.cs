using Microsoft.EntityFrameworkCore.Query;
using MiniMvcProject.Application.ViewModels.Generic;
using MiniMvcProject.Domain.Entities;
using System.Linq.Expressions;

namespace MiniMvcProject.Application.Services.Abstractions.Generic
{
    public interface ICrudService<T, TVm, TCrVm, TUpVm>
        where T : BaseEntity
        where TVm : IViewModel
        where TCrVm : IViewModel
        where TUpVm : IViewModel
    {
        Task<ResultViewModel<TVm>> GetAsync(int id);

        Task<ResultViewModel<TVm>> GetAsync(Expression<Func<T, bool>> predicate,
                                   Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, bool enableTracking = true);
        Task<ResultViewModel<IEnumerable<TVm>>> GetListAsync(Expression<Func<T, bool>>? predicate = null,
                                         Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
                                         Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                                         int index = 0, int size = int.MaxValue, bool enableTracking = true);
        Task<ResultViewModel<TVm>> CreateAsync(TCrVm createViewModel);
        Task<ResultViewModel<TVm>> UpdateAsync(TUpVm entity);
        Task<ResultViewModel<TVm>> RemoveAsync(int id);
        Task<ResultViewModel<TUpVm>> GetUpdateViewModel(Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);
        Task SaveChangesAsync();
    }
}
