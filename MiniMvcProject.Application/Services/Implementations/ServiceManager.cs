using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniMvcProject.Application.Services.Abstractions;
using MiniMvcProject.Application.Services.Implementations.Generic;
using MiniMvcProject.Application.Utilities;
using MiniMvcProject.Application.ViewModels.Generic;
using MiniMvcProject.Application.ViewModels.ServiceViewModels;
using MiniMvcProject.Application.ViewModels.SliderViewModels;
using MiniMvcProject.Domain.Entities;
using MiniMvcProject.Persistance.Repositories.Abstractions.Generic;

namespace MiniMvcProject.Application.Services.Implementations
{
    public class ServiceManager : CrudManager<Service, ServiceViewModel, ServiceCreateViewModel, ServiceUpdateViewModel>, IServiceService
    {
        private readonly ICloudinaryService _cloudinaryService;

        public ServiceManager(IRepository<Service> repository, IMapper mapper, ICloudinaryService cloudinaryService) : base(repository, mapper)
        {
            _cloudinaryService = cloudinaryService;
        }

        public override async Task<ResultViewModel<ServiceViewModel>> CreateAsync(ServiceCreateViewModel vm)
        {

            var imageCheck = _validate(vm.IconImage);

            if (imageCheck != null) return imageCheck;

            vm.IconUrl = await _cloudinaryService.ImageCreateAsync(vm.IconImage);


            return await base.CreateAsync(vm);
        }


        public override async Task<ResultViewModel<ServiceViewModel>> UpdateAsync(ServiceUpdateViewModel vm)
        {
            if (vm.IconImage != null)
            {
                var imageCheck = _validate(vm.IconImage);
                if (imageCheck != null) return imageCheck;
                _cloudinaryService.ImageDelete((await base.GetAsync(x => x.Id == vm.Id, enableTracking: false)).Data.IconUrl);
                vm.IconUrl = await _cloudinaryService.ImageCreateAsync(vm.IconImage);
            }


            return await base.UpdateAsync(vm);
        }

        private ResultViewModel<ServiceViewModel>? _validate(IFormFile val)
        {
            if (!val.CheckType())
            {
                return new ResultViewModel<ServiceViewModel>
                {
                    Success = false,
                    Message = "Cannot be non-image file."
                };

            }

            return null;
        }
    }


}
