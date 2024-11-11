using MiniMvcProject.Application.Services.Abstractions.Generic;
using MiniMvcProject.Application.ViewModels.SliderViewModels;
using MiniMvcProject.Domain.Entities;

namespace MiniMvcProject.Application.Services.Abstractions
{

    public interface ISliderService : ICrudService<Slider, SliderViewModel, SliderCreateViewModel, SliderUpdateViewModel>
    {
    }
}
