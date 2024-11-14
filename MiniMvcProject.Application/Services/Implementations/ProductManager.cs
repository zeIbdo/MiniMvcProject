using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiniMvcProject.Application.Services.Abstractions;
using MiniMvcProject.Application.Services.Implementations.Generic;
using MiniMvcProject.Application.ViewModels.CategoryViewModels;
using MiniMvcProject.Application.ViewModels.Generic;
using MiniMvcProject.Application.ViewModels.ProductImageViewModels;
using MiniMvcProject.Application.ViewModels.ProductTagViewModels;
using MiniMvcProject.Application.ViewModels.ProductViewModels;
using MiniMvcProject.Application.ViewModels.TagViewModels;
using MiniMvcProject.Domain.Entities;
using MiniMvcProject.Persistance.Repositories.Abstractions.Generic;

namespace MiniMvcProject.Application.Services.Implementations
{
    public class ProductManager : CrudManager<Product, ProductViewModel, ProductCreateViewModel, ProductUpdateViewModel>, IProductService
    {
        private readonly ICategoryService _categoryService;
        private readonly ITagService _tagService;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IRepository<Product> _repository;
        private readonly IProductTagService _productTagService;
        private readonly IMapper _mapper;
        public ProductManager(IRepository<Product> repository, IMapper mapper, ICategoryService categoryService, ITagService tagService, ICloudinaryService cloudinaryService, IProductTagService productTagService) : base(repository, mapper)
        {
            _categoryService = categoryService;
            _tagService = tagService;
            _cloudinaryService = cloudinaryService;
            _repository = repository;
            _mapper = mapper;
            _productTagService = productTagService;
        }

        public async Task<ProductCreateViewModel> GetProductCreateViewModelAsync(ProductCreateViewModel productCreateViewModel)
        {
            var categoryList = await _categoryService.GetListAsync();
            var categorySelectListItems = new List<SelectListItem>();

            foreach (var category in categoryList.Data ?? new List<CategoryViewModel>())
            {
                categorySelectListItems.Add(new SelectListItem(category.Name, category.Id.ToString()));
            }

            var tagList = await _tagService.GetListAsync();
            var tagSelectListItems = new List<SelectListItem>();

            foreach (var tag in tagList.Data ?? new List<TagViewModel>())
            {
                tagSelectListItems.Add(new SelectListItem(tag.Name, tag.Id.ToString()));
            }

            productCreateViewModel.Tags = tagSelectListItems;
            productCreateViewModel.Categories = categorySelectListItems;

            return productCreateViewModel;
        }


        public async Task<ResultViewModel<ProductUpdateViewModel>> GetProductUpdateViewModelAsync(int id)
        {
            var product = await _repository.GetAsync(x => x.Id == id, include: x => x.Include(x => x.ProductTags).ThenInclude(y => y.Tag), enableTracking: false);
            if (product == null)
                return new ResultViewModel<ProductUpdateViewModel>()
                {
                    Success = false,
                    Message = "Not Found"
                };
            var categories = await _categoryService.GetListAsync();
            var categoryListItems = new List<SelectListItem>();

            foreach (var category in categories.Data ?? new List<CategoryViewModel>())
            {
                categoryListItems.Add(new SelectListItem(category.Name, category.Id.ToString(), category.Id == product.CategoryId));
            }

            var oldTagItems = new List<SelectListItem>();
            foreach (var prTag in product.ProductTags)
            {
                oldTagItems.Add(new SelectListItem(prTag.Tag.Name, prTag.Tag.Id.ToString()));
            }
            var allTags = await _tagService.GetListAsync();
            var newTagItems = new List<SelectListItem>();


            foreach (var item in allTags.Data ?? new List<TagViewModel>())
            {
                if (oldTagItems.Any(x => x.Value == item.Id.ToString()))
                    continue;

                newTagItems.Add(new SelectListItem(item.Name, item.Id.ToString()));
            }
            var vm = new ProductUpdateViewModel()
            {
                Categories = categoryListItems,

                OldTags = oldTagItems,
                NewTags = newTagItems
            };
            vm = _mapper.Map(product,vm);
            return new ResultViewModel<ProductUpdateViewModel>() { Data = vm, Success = true };
        }


        public override async Task<ResultViewModel<ProductViewModel>> UpdateAsync(ProductUpdateViewModel vm)
        {
            var product = await _repository.GetAsync(vm.Id);

            if (product == null)
                return new ResultViewModel<ProductViewModel>() { Message = "not found" };

            product = _mapper.Map(vm,product);

            foreach(var item in vm.OldTagIds ?? new List<int>())
            {   
                var deletedTag = await _productTagService.GetAsync(x=> x.ProductId==vm.Id&&x.TagId==item);

                if (deletedTag.Data == null) continue;

                await _productTagService.RemoveAsync(deletedTag.Data.Id);
            }

            foreach (var item in vm.NewTagIds ?? new List<int>())
            {
                var prTag = new ProductTagCreateViewModel() { ProductId = vm.Id, TagId = item };
                await _productTagService.CreateAsync(prTag);
            }

            var result = await _repository.UpdateAsync(product);
            return new ResultViewModel<ProductViewModel>() { Success = true, Data = _mapper.Map<ProductViewModel>(result) };
        }


        public override async Task<ResultViewModel<ProductViewModel>> CreateAsync(ProductCreateViewModel createViewModel)
        {
            var result = _validate(createViewModel.MainPrice);
            if (result != null) return result;

            result = _validate(createViewModel.DiscountPrice);
            if (result != null) return result;

            result = _validate(createViewModel.RewardPoints);
            if (result != null) return result;

            result = _validate(createViewModel.StockAmount);
            if (result != null) return result;

            result = _validate(createViewModel.Rating);
            if (result != null) return result;


            return await base.CreateAsync(createViewModel);
        }



        private ResultViewModel<ProductViewModel>? _validate(int val)
        {
            if (val < 0)
            {
                return new ResultViewModel<ProductViewModel>
                {
                    Success = false,
                    Message = "Cannot be a negative number."
                };
            }
            return null;
        }
        private ResultViewModel<ProductViewModel>? _validate(decimal val)
        {
            if (val < 0)
            {
                return new ResultViewModel<ProductViewModel>
                {
                    Success = false,
                    Message = "Cannot be a negative number."
                };
            }
            return null;
        }

        public async Task<ResultViewModel<ProductViewModel>> SoftDeleteProductAsync(int id)
        {
            var product = await _repository.GetAsync(x => x.Id == id);

            if (product == null)
            {
                return new ResultViewModel<ProductViewModel>
                {
                    Success = false,
                    Message = "Product not found."
                };
            }

            product.IsDeleted = true;

            await _repository.UpdateAsync(product);

            return new ResultViewModel<ProductViewModel>
            {
                Success = true,
                Data = _mapper.Map<ProductViewModel>(product)
            };
        }

    }


}
