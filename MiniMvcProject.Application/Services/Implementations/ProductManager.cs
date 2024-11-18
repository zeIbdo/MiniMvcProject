using AutoMapper;
using core.Persistance.Paging;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using MiniMvcProject.Application.Services.Abstractions;
using MiniMvcProject.Application.Services.Implementations.Generic;
using MiniMvcProject.Application.ViewModels.CategoryViewModels;
using MiniMvcProject.Application.ViewModels.Generic;
using MiniMvcProject.Application.ViewModels.ProductImageViewModels;
using MiniMvcProject.Application.ViewModels.ProductTagViewModels;
using MiniMvcProject.Application.ViewModels.ProductViewModels;
using MiniMvcProject.Application.ViewModels.TagViewModels;
using MiniMvcProject.Domain.Entities;
using MiniMvcProject.Persistance.Repositories.Abstractions;
using MiniMvcProject.Persistance.Repositories.Abstractions.Generic;
using System.Linq.Expressions;

namespace MiniMvcProject.Application.Services.Implementations
{

    public class ProductManager : CrudManager<Product, ProductViewModel, ProductCreateViewModel, ProductUpdateViewModel>, IProductService
    {
        private readonly IEmailService _emailService;
        private readonly ISubscriptionService _subscriptionService;
        private readonly ICategoryService _categoryService;
        private readonly ITagService _tagService;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IRepository<Product> _repository;
        private readonly IProductRepository _productRepository;
        private readonly IProductTagService _productTagService;
        private readonly IProductImageService _productImageService;
        private readonly IMapper _mapper;
        public ProductManager(IRepository<Product> repository, IMapper mapper, ICategoryService categoryService, ITagService tagService, ICloudinaryService cloudinaryService, IProductTagService productTagService, IProductImageService productImageService, ISubscriptionService subscriptionService, IEmailService emailService, IProductRepository productRepository) : base(repository, mapper)
        {
            _categoryService = categoryService;
            _tagService = tagService;
            _cloudinaryService = cloudinaryService;
            _repository = repository;
            _mapper = mapper;
            _productTagService = productTagService;
            _productImageService = productImageService;
            _subscriptionService = subscriptionService;
            _emailService = emailService;
            _productRepository = productRepository;
        }

        public async Task<ProductCreateViewModel> GetProductCreateViewModelAsync(ProductCreateViewModel productCreateViewModel)
        {
            var categoryList = await _categoryService.GetListAsync();
            var categorySelectListItems = new List<SelectListItem>();

            foreach (var category in categoryList.Data ?? new List<CategoryViewModel>())
            {
                categorySelectListItems.Add(new SelectListItem(category.Name, category.Id.ToString(), false));
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
            var product = await _repository.GetAsync(x => x.Id == id, include: x => x.Include(x => x.ProductTags).ThenInclude(y => y.Tag).Include(x => x.ProductImages), enableTracking: false);
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
                Brand = null!,
                Code = null!,
                Description = null!,
                Name = null!,
                OldTags = oldTagItems,
                NewTags = newTagItems
            };

            var mainImgUrl = product.ProductImages.FirstOrDefault(x => x.IsMain == true)!.ImageUrl;
            var hoverImgUrl = product.ProductImages.FirstOrDefault(x => x.IsSecondary == true)!.ImageUrl;
            var additionalUrls = new List<string>();
            var additionalIds = new List<int>();
            foreach (var image in product.ProductImages)
            {
                if (image.IsMain == false && image.IsSecondary == false)
                {
                    additionalUrls.Add(image.ImageUrl);
                    additionalIds.Add(image.Id);
                }
            }
            vm.AdditonalImageUrls = additionalUrls;
            vm.MainImageUrl = mainImgUrl;
            vm.SecondaryImageUrl = hoverImgUrl;
            vm.AdditonalImageIds = additionalIds;
            vm = _mapper.Map(product, vm);
            return new ResultViewModel<ProductUpdateViewModel>() { Data = vm, Success = true };
        }


        public override async Task<ResultViewModel<ProductViewModel>> UpdateAsync(ProductUpdateViewModel vm)
        {

            if (vm.Name.Length < 3)
                return new ResultViewModel<ProductViewModel> { Success = false, Message = "name must be at least 3 char" };

            if (!(vm.Rating <= 5 && vm.Rating >= 0))
                return new ResultViewModel<ProductViewModel> { Success = false, Message = "Rating must be between 0 and 5" };

            var result = _validate(vm.MainPrice);
            if (result != null) return result;

            result = _validate(vm.DiscountPrice);
            if (result != null) return result;

            if (vm.DiscountPrice >= vm.MainPrice)
                return new ResultViewModel<ProductViewModel> { Success = false, Message = "Discount price cannot be higher or equal to main price(If you dont want to give discount just give 0)" };

            result = _validate(vm.RewardPoints);
            if (result != null) return result;

            result = _validate(vm.StockAmount);
            if (result != null) return result;


            var product = await _repository.GetAsync(x => x.Id == vm.Id, include: x => x.Include(x => x.ProductImages));

            if (product == null)
                return new ResultViewModel<ProductViewModel>() { Message = "not found" };

            product = _mapper.Map(vm, product);

            foreach (var item in vm.OldTagIds ?? new List<int>())
            {
                var deletedTag = await _productTagService.GetAsync(x => x.ProductId == vm.Id && x.TagId == item);

                if (deletedTag.Data == null) continue;

                await _productTagService.RemoveAsync(deletedTag.Data.Id);
            }

            foreach (var item in vm.NewTagIds ?? new List<int>())
            {
                var prTag = new ProductTagCreateViewModel() { ProductId = vm.Id, TagId = item };
                await _productTagService.CreateAsync(prTag);
            }

            if (vm.MainImage != null)
            {
                var existingMainImage = product.ProductImages.FirstOrDefault(x => x.IsMain == true);
                _cloudinaryService.ImageDelete(existingMainImage!.ImageUrl);
                string newMainUrl = await _cloudinaryService.ImageCreateAsync(vm.MainImage);
                existingMainImage.ImageUrl = newMainUrl;
                await _productImageService.SaveChangesAsync();
            }
            if (vm.HoverImage != null)
            {
                var existingHoverImage = product.ProductImages.FirstOrDefault(x => x.IsSecondary == true);
                _cloudinaryService.ImageDelete(existingHoverImage!.ImageUrl);
                string newHoverUrl = await _cloudinaryService.ImageCreateAsync(vm.HoverImage);
                existingHoverImage.ImageUrl = newHoverUrl;
                await _productImageService.SaveChangesAsync();
            }

            var existingAdditionalImages = product.ProductImages
                .Where(x => !x.IsMain && !x.IsSecondary)
                .ToList();

            var imageIdsToDelete = vm.ImagesToDelete?.Split(',').Select(int.Parse).ToList() ?? new List<int>();

            foreach (var imageId in imageIdsToDelete)
            {
                var additionalImage = product.ProductImages.FirstOrDefault(x => x.Id == imageId);
                if (additionalImage != null)
                {
                    _cloudinaryService.ImageDelete(additionalImage.ImageUrl);
                    await _productImageService.RemoveAsync(additionalImage.Id);
                }
            }

            if (vm.AdditionalImages != null)
            {
                foreach (var image in vm.AdditionalImages)
                {
                    var newUrl = await _cloudinaryService.ImageCreateAsync(image);
                    await _productImageService.CreateAsync(new ProductImageCreateViewModel { ImageUrl = newUrl, ProductId = product.Id });

                }

            }

            var resultProduct = await _repository.UpdateAsync(product);
            return new ResultViewModel<ProductViewModel>() { Success = true, Data = _mapper.Map<ProductViewModel>(result) };
        }


        public override async Task<ResultViewModel<ProductViewModel>> CreateAsync(ProductCreateViewModel createViewModel)
        {
            if (createViewModel.Name.Length < 3)
                return new ResultViewModel<ProductViewModel> { Success = false, Message = "name must be at least 3 char" };


            if (!(createViewModel.Rating <= 5 && createViewModel.Rating >= 0))
                return new ResultViewModel<ProductViewModel> { Success = false, Message = "Rating must be between 0 and 5" };

            var result = _validate(createViewModel.MainPrice);
            if (result != null) return result;           

            result = _validate(createViewModel.DiscountPrice);
            if (result != null) return result;

            if(createViewModel.DiscountPrice>=createViewModel.MainPrice)
                return new ResultViewModel<ProductViewModel> { Success = false, Message = "Discount price cannot be higher or equal to main price(If you dont want to give discount just give 0)" };

            result = _validate(createViewModel.RewardPoints);
            if (result != null) return result;

            result = _validate(createViewModel.StockAmount);
            if (result != null) return result;



            var subscribers = await _subscriptionService.GetListAsync(enableTracking: false);

            if ((await _categoryService.GetAsync(x => x.Id == createViewModel.CategoryId, enableTracking: false)).Data == null)
                return new ResultViewModel<ProductViewModel> { Success = false, Message = "Invalid category id" };

            var createdProductResult = await base.CreateAsync(createViewModel);


            createViewModel.MainImageUrl = await _cloudinaryService.ImageCreateAsync(createViewModel.MainImage);
            createViewModel.SecondaryImageUrl = await _cloudinaryService.ImageCreateAsync(createViewModel.HoverImage);
            foreach (var img in createViewModel.AdditionalImages)
            {
                var addUrl = await _cloudinaryService.ImageCreateAsync(img);
                createViewModel.additonalImageUrls.Add(addUrl);
                await _productImageService.CreateAsync(new ProductImageCreateViewModel { ImageUrl = addUrl, ProductId = createdProductResult.Data!.Id });

            }
            if (subscribers.Data != null)
            {
                foreach (var sub in subscribers.Data)
                {
                    if (sub.Email is { })
                    {

                        _emailService.SendEmail(sub.Email, $"🎉 Exciting News! Introducing Our New Product: {createViewModel.Name}", 
                            $"<!DOCTYPE html>\r\n<html>\r\n<head>\r\n    <style>\r\n        body {{\r\n            font-family: Arial, sans-serif;\r\n            line-height: 1.6;\r\n            color: #333333;\r\n            background-color: #f9f9f9;\r\n            padding: 20px;\r\n        }}\r\n        .email-container {{\r\n            max-width: 600px;\r\n            margin: 0 auto;\r\n            background: #ffffff;\r\n            border: 1px solid #dddddd;\r\n            border-radius: 8px;\r\n            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);\r\n            padding: 20px;\r\n        }}\r\n        .header {{\r\n            text-align: center;\r\n            color: #ffffff;\r\n            background-color: #4CAF50;\r\n            padding: 10px 0;\r\n            border-radius: 8px 8px 0 0;\r\n        }}\r\n        .header h1 {{\r\n            margin: 0;\r\n            font-size: 24px;\r\n        }}\r\n        .content {{\r\n            padding: 20px;\r\n        }}\r\n        .content h2 {{\r\n            color: #4CAF50;\r\n            font-size: 22px;\r\n        }}\r\n        .content p {{\r\n            font-size: 16px;\r\n            margin: 10px 0;\r\n        }}\r\n        .cta {{\r\n            display: inline-block;\r\n            margin-top: 20px;\r\n            padding: 10px 20px;\r\n            background-color: #4CAF50;\r\n            color: #ffffff;\r\n            text-decoration: none;\r\n            border-radius: 4px;\r\n            font-size: 16px;\r\n        }}\r\n        .cta:hover {{\r\n            background-color: #45a049;\r\n        }}\r\n        .footer {{\r\n            text-align: center;\r\n            font-size: 12px;\r\n            color: #888888;\r\n            margin-top: 20px;\r\n        }}\r\n    </style>\r\n</head>\r\n<body>\r\n    <div class=\"email-container\">\r\n        <div class=\"header\">\r\n            <h1>🎉 Exciting Announcement!</h1>\r\n        </div>\r\n        <div class=\"content\">\r\n            <h2>Introducing: {createViewModel.Name}</h2>\r\n            <p>We are thrilled to share our latest creation with you—our brand-new product, <strong>{createViewModel.Name}</strong>. Designed with care, it brings innovation and value to meet your needs.</p>\r\n            <p>Stay tuned for more updates and explore the possibilities this product has to offer!</p>\r\n           <img style=\"height:300px; width:200px;\" src=\"{createViewModel.MainImageUrl}\"        </div>\r\n        <div class=\"footer\">\r\n            <p>Thank you for being part of our journey. If you have any questions, feel free to reach out!</p>\r\n        </div>\r\n    </div>\r\n</body>\r\n</html>");
                    }
                }
            }
            await _productImageService.CreateAsync(new ProductImageCreateViewModel { ImageUrl = createViewModel.MainImageUrl, IsMain = true, ProductId = createdProductResult.Data!.Id });
            await _productImageService.CreateAsync(new ProductImageCreateViewModel { ImageUrl = createViewModel.SecondaryImageUrl, IsSecondary = true, ProductId = createdProductResult.Data.Id });
            return createdProductResult;
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

        public async Task IncrementViewCountAsync(int id)
        {
            var product = await _repository.GetAsync(id);
            product!.ViewCount++;
            await _repository.UpdateAsync(product);
        }

        public async Task<Paginate<ProductViewModel>> GetPaginatedProductsAsync(Expression<Func<Product, bool>>? predicate = null, Func<IQueryable<Product>, IIncludableQueryable<Product, object>>? include = null, Func<IQueryable<Product>, IOrderedQueryable<Product>>? orderBy = null, int index = 0, int size = int.MaxValue)
        {
            var products = await _productRepository.GetPaginatedProductsAsync(predicate, include, orderBy, index, size);
            return _mapper.Map<Paginate<ProductViewModel>>(products);
        }
    }


}
