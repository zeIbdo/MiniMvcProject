using AutoMapper;
using MiniMvcProject.Application.Services.Abstractions;
using MiniMvcProject.Application.Services.Implementations.Generic;
using MiniMvcProject.Application.ViewModels.TagViewModels;
using MiniMvcProject.Domain.Entities;
using MiniMvcProject.Persistance.Repositories.Abstractions.Generic;

namespace MiniMvcProject.Application.Services.Implementations
{
    public class TagManager : CrudManager<Tag, TagViewModel, TagCreateViewModel, TagUpdateViewModel>, ITagService
    {
        public TagManager(IRepository<Tag> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }


}
