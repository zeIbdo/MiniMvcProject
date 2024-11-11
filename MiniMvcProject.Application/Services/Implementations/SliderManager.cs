using AutoMapper;
using MiniMvcProject.Application.Services.Abstractions;
using MiniMvcProject.Application.Services.Implementations.Generic;
using MiniMvcProject.Application.ViewModels.SliderViewModels;
using MiniMvcProject.Domain.Entities;
using MiniMvcProject.Persistance.Repositories.Abstractions.Generic;

namespace MiniMvcProject.Application.Services.Implementations
{
    public class SliderManager : CrudManager<Slider, SliderViewModel, SliderCreateViewModel, SliderUpdateViewModel>, ISliderService
    {
        public SliderManager(IRepository<Slider> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }


}
