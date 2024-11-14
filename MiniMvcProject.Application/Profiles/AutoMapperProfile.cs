using AutoMapper;
using core.Persistance.Paging;
using MiniMvcProject.Application.UI.ViewModels;
using MiniMvcProject.Application.ViewModels.AppUserViewModels;
using MiniMvcProject.Application.ViewModels.BasketItemViewModels;
using MiniMvcProject.Application.ViewModels.CategoryViewModels;
using MiniMvcProject.Application.ViewModels.ProductImageViewModels;
using MiniMvcProject.Application.ViewModels.ProductTagViewModels;
using MiniMvcProject.Application.ViewModels.ProductViewModels;
using MiniMvcProject.Application.ViewModels.ServiceViewModels;
using MiniMvcProject.Application.ViewModels.SettingViewModels;
using MiniMvcProject.Application.ViewModels.SliderViewModels;
using MiniMvcProject.Application.ViewModels.SubscriptionViewModels;
using MiniMvcProject.Application.ViewModels.TagViewModels;
using MiniMvcProject.Domain.Entities;

namespace MiniMvcProject.Application.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            CreateMap<BasketItemViewModel, BasketItemUpdateViewModel>();


            CreateMap<AppUser, RegisterViewModel>().ReverseMap();
            CreateMap<AppUser, LoginViewModel>().ReverseMap();

            CreateMap<Slider, SliderViewModel>().ReverseMap();
            CreateMap<Slider, SliderCreateViewModel>().ReverseMap();
            CreateMap<Slider, SliderUpdateViewModel>().ReverseMap();

            CreateMap<Setting, SettingViewModel>().ReverseMap();
            CreateMap<Setting, SettingCreateViewModel>().ReverseMap();
            CreateMap<Setting, SettingUpdateViewModel>().ReverseMap();

            CreateMap<Subscription, SubscriptionViewModel>().ReverseMap();
            CreateMap<Subscription, SubscriptionCreateViewModel>().ReverseMap();
            CreateMap<Subscription, SubscriptionUpdateViewModel>().ReverseMap();

            CreateMap<ProductTag, ProductTagViewModel>()
                .ReverseMap();
            CreateMap<ProductTag, ProductTagCreateViewModel>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.TagId, opt => opt.MapFrom(src => src.TagId))
                .ReverseMap();
            CreateMap<ProductTag, ProductTagUpdateViewModel>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.TagId, opt => opt.MapFrom(src => src.TagId))
                .ReverseMap();

            CreateMap<Tag, TagViewModel>().ReverseMap();
            CreateMap<Tag, TagCreateViewModel>().ReverseMap();
            CreateMap<Tag, TagUpdateViewModel>().ReverseMap();

            CreateMap<ProductImage, ProductImageViewModel>().ReverseMap();
            CreateMap<ProductImage, ProductImageCreateViewModel>().ReverseMap();
            CreateMap<ProductImage, ProductImageUpdateViewModel>().ReverseMap();

            CreateMap<Service, ServiceViewModel>().ReverseMap();
            CreateMap<Service, ServiceCreateViewModel>().ReverseMap();
            CreateMap<Service, ServiceUpdateViewModel>().ReverseMap();

            CreateMap<Category, CategoryViewModel>()
                .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.ParentCategoryId))
                .ReverseMap();
            CreateMap<Category, CategoryCreateViewModel>()
                .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.ParentCategoryId))
                .ReverseMap();
            CreateMap<Category, CategoryUpdateViewModel>()
                .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.ParentCategoryId))
                .ReverseMap();

            CreateMap<Product, ProductViewModel>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.ProductImages))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.ProductTags.Select(x => x.Tag)))
                .ReverseMap();
            CreateMap< ProductCreateViewModel,Product>()
                .ForMember(d => d.ProductTags, x => x.MapFrom(s => s.TagIds.Select(x => new ProductTag { TagId=x})))
                .ReverseMap();
            CreateMap<Product, ProductUpdateViewModel>()
                //.ForMember(dest => dest.OldTagIds, opt => opt.MapFrom(src => src.ProductTags.Select(pt => pt.TagId).ToList()))
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.ProductImages))
                .ReverseMap();

            CreateMap<BasketItem, BasketItemViewModel>()
                .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product))
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.AppUserName, opt => opt.MapFrom(src => src.AppUserId))
                .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Count))
                .ReverseMap();
            CreateMap<BasketItem, BasketItemCreateViewModel>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.AppUserName, opt => opt.MapFrom(src => src.AppUserId))
                .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Count))
                .ReverseMap();
            CreateMap<BasketItem, BasketItemUpdateViewModel>().ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.AppUserName, opt => opt.MapFrom(src => src.AppUserId))
                .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Count))
                .ReverseMap();
            CreateMap<AppUser, AppUserViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ReverseMap();



            CreateMap<Paginate<Category>, List<CategoryViewModel>>()
           .ConvertUsing((src, dest, context) =>
               src.Items.Select(item => context.Mapper.Map<CategoryViewModel>(item)).ToList());
            CreateMap<Paginate<Tag>, List<TagViewModel>>()
           .ConvertUsing((src, dest, context) =>
               src.Items.Select(item => context.Mapper.Map<TagViewModel>(item)).ToList());
            CreateMap<Paginate<BasketItem>, List<BasketItemViewModel>>()
           .ConvertUsing((src, dest, context) =>
               src.Items.Select(item => context.Mapper.Map<BasketItemViewModel>(item)).ToList());
            CreateMap<Paginate<Product>, List<ProductViewModel>>()
           .ConvertUsing((src, dest, context) =>
               src.Items.Select(item => context.Mapper.Map<ProductViewModel>(item)).ToList());

            CreateMap<BasketItemViewModel, BasketViewModel>()
           .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
           .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.Product.MainPrice))
           .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
           .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Count)).ReverseMap();
        }
    }
}
