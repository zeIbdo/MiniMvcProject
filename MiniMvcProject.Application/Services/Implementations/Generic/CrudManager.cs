using AutoMapper;
using Microsoft.EntityFrameworkCore.Query;
using MiniMvcProject.Application.Services.Abstractions.Generic;
using MiniMvcProject.Application.ViewModels.Generic;
using MiniMvcProject.Application.ViewModels.TagViewModels;
using MiniMvcProject.Domain.Entities;
using MiniMvcProject.Persistance.Repositories.Abstractions.Generic;
using System.Linq.Expressions;

namespace MiniMvcProject.Application.Services.Implementations.Generic
{
    public class CrudManager<T, TVm, TCrVm, TUpVm> : ICrudService<T, TVm, TCrVm, TUpVm>
        where T : BaseEntity
        where TVm : IViewModel
        where TCrVm : IViewModel
        where TUpVm : IViewModel
    {

        private readonly IRepository<T> _repository;
        private readonly IMapper _mapper;

        public CrudManager(IRepository<T> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual async Task<ResultViewModel<TVm>> CreateAsync(TCrVm createViewModel)
        {
            var entity = _mapper.Map<T>(createViewModel);

            await GetAsync(entity.Id);

            var createdEntity = await _repository.AddAsync(entity);

            var viewModel = _mapper.Map<TVm>(createdEntity);

            return new ResultViewModel<TVm> { Data = viewModel };
        }

        public virtual async Task<ResultViewModel<TVm>> GetAsync(int id)
        {
            var entity = await _repository.GetAsync(id);

            if (entity == null)
                return new ResultViewModel<TVm> { Success = false };

            var viewModel = _mapper.Map<TVm>(entity);

            return new ResultViewModel<TVm> { Data = viewModel };
        }

        public virtual async Task<ResultViewModel<TVm>> GetAsync(Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, bool enableTracking = true)
        {
            var entity = await _repository.GetAsync(predicate, include,enableTracking);

            if (entity == null)
                return new ResultViewModel<TVm> { Success = false };

            return new ResultViewModel<TVm> { Data = _mapper.Map<TVm>(entity) };
        }

        public virtual async Task<ResultViewModel<IEnumerable<TVm>>> GetListAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>,
            IIncludableQueryable<T, object>>? include = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            int index = 0, int size = int.MaxValue, bool enableTracking = true)
        {
            var entityList = await _repository.GetListAsync(predicate, orderBy, include,index,size,enableTracking);

            if (entityList == null)
                return new ResultViewModel<IEnumerable<TVm>> { Success = false,Message="Not Found" };

            return new ResultViewModel<IEnumerable<TVm>> { Data = _mapper.Map<List<TVm>>(entityList) };

        }

        public async Task<ResultViewModel<TUpVm>> GetUpdateViewModel(Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
        {
            var result = await _repository.GetAsync(predicate,include,enableTracking:false);

            if(result == null)
                return new ResultViewModel<TUpVm> { Success = false,Message="Not Found" };

            return new ResultViewModel<TUpVm> {Data=_mapper.Map<TUpVm>(result) };
        }

        public virtual async Task<ResultViewModel<TVm>> RemoveAsync(int id)
        {
            var result = await _repository.GetAsync(id);

            if (result == null)
                return new ResultViewModel<TVm> { Success = false,Message="Not Found" };

            var entity = await _repository.DeleteAsync(result);
            return new ResultViewModel<TVm> { Data = _mapper.Map<TVm>(entity) };
        }

        public async Task SaveChangesAsync()
        {
            await _repository.SaveChangesAsync();
        }

        public virtual async Task<ResultViewModel<TVm>> UpdateAsync(TUpVm vm)
        {
            var entity = _mapper.Map<T>(vm);

            await GetAsync(e=>e.Id==entity.Id,enableTracking:false);

            var updatedEntity = await _repository.UpdateAsync(entity);

            var viewModel = _mapper.Map<TVm>(updatedEntity);

            return new ResultViewModel<TVm> { Data = viewModel };
        }
    }
}
