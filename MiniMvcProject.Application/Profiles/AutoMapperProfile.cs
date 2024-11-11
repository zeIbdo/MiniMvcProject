using AutoMapper;
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
                .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product))
                .ForMember(dest => dest.Tag, opt => opt.MapFrom(src => src.Tag))
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
            CreateMap<Product, ProductCreateViewModel>()
                .ForMember(dest => dest.TagIds, opt => opt.MapFrom(src => src.ProductTags.Select(x => x.Tag)))
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.ProductImages))
                .ReverseMap();
            CreateMap<Product, ProductUpdateViewModel>()
                .ForMember(dest => dest.OldTagIds, opt => opt.MapFrom(src => src.ProductTags.Select(pt => pt.TagId).ToList()))
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.ProductImages))
                .ReverseMap();

            CreateMap<BasketItem, BasketItemViewModel>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.AppUserId, opt => opt.MapFrom(src => src.AppUserId))
                .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Count))
                .ReverseMap();
            CreateMap<BasketItem, BasketItemCreateViewModel>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.AppUserId, opt => opt.MapFrom(src => src.AppUserId))
                .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Count))
                .ReverseMap();
            CreateMap<BasketItem, BasketItemUpdateViewModel>().ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.AppUserId, opt => opt.MapFrom(src => src.AppUserId))
                .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Count))
                .ReverseMap();
            CreateMap<AppUser, AppUserViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ReverseMap();
        }
    }
}
