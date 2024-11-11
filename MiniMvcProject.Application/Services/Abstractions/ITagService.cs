using MiniMvcProject.Application.Services.Abstractions.Generic;
using MiniMvcProject.Application.ViewModels.TagViewModels;
using MiniMvcProject.Domain.Entities;

namespace MiniMvcProject.Application.Services.Abstractions
{
    public interface ITagService : ICrudService<Tag, TagViewModel, TagCreateViewModel, TagUpdateViewModel>
    {
    }
}
