using AutoMapper;
using Microsoft.AspNetCore.Http;
using MiniMvcProject.Application.Services.Abstractions;
using MiniMvcProject.Application.Services.Implementations.Generic;
using MiniMvcProject.Application.Utilities;
using MiniMvcProject.Application.ViewModels.Generic;
using MiniMvcProject.Application.ViewModels.SliderViewModels;
using MiniMvcProject.Domain.Entities;
using MiniMvcProject.Persistance.Repositories.Abstractions.Generic;

namespace MiniMvcProject.Application.Services.Implementations
{
    public class SliderManager : CrudManager<Slider, SliderViewModel, SliderCreateViewModel, SliderUpdateViewModel>, ISliderService
    {
        private readonly ICloudinaryService _cloudinaryService;
        public SliderManager(IRepository<Slider> repository, IMapper mapper, ICloudinaryService cloudinaryService) : base(repository, mapper)
        {
            _cloudinaryService = cloudinaryService;
        }

        public override async Task<ResultViewModel<SliderViewModel>> CreateAsync(SliderCreateViewModel createViewModel)
        {

            var imageCheck = _validate(createViewModel.Image);

            if (imageCheck != null) return imageCheck;

            createViewModel.ImageUrl = await _cloudinaryService.ImageCreateAsync(createViewModel.Image);

            var result = _validate(createViewModel.Price);

            if (result != null) return result;

            return await base.CreateAsync(createViewModel);
        }
        public override async Task<ResultViewModel<SliderViewModel>> UpdateAsync(SliderUpdateViewModel vm)
        {
            if (vm.Image != null)
            {
                var imageCheck = _validate(vm.Image);
                if (imageCheck != null) return imageCheck;
                _cloudinaryService.ImageDelete(vm.ImageUrl);
                vm.ImageUrl = await _cloudinaryService.ImageCreateAsync(vm.Image);
            }

            //vm.ImageUrl=(await base.GetAsync(x=>x.Id==vm.Id,enableTracking:false)).Data.ImageUrl;

            var result = _validate(vm.Price);

            if (result != null) return result;

            return await base.UpdateAsync(vm);
        }

        private ResultViewModel<SliderViewModel>? _validate(decimal val)
        {
            if (val < 0)
            {
                return new ResultViewModel<SliderViewModel>
                {
                    Success = false,
                    Message = "Cannot be a negative number."
                };
            }
            return null;
        }

        private ResultViewModel<SliderViewModel>? _validate(IFormFile val)
        {
            if (!val.CheckType())
            {
                return new ResultViewModel<SliderViewModel>
                {
                    Success = false,
                    Message = "Cannot be non-image file."
                };

            }

            return null;
        }

        public override async Task<ResultViewModel<SliderViewModel>> RemoveAsync(int id)
        {
            var slider = (await GetAsync(x=>x.Id==id)).Data;

            _cloudinaryService.ImageDelete(slider!.ImageUrl!);

            return await base.RemoveAsync(id);
        }

    }


}
