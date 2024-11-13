using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MiniMvcProject.Application.Services.Abstractions;
using MiniMvcProject.Application.Services.Abstractions.Generic;
using MiniMvcProject.Application.Services.Implementations;
using MiniMvcProject.Application.Services.Implementations.Generic;
using MiniMvcProject.DAL.DataContext;
using MiniMvcProject.Domain.Entities;
using System.Reflection;

namespace MiniMvcProject.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped(typeof(ICrudService<,,,>), typeof(CrudManager<,,,>));
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<IProductService, ProductManager>();
            services.AddScoped<IServiceService, ServiceManager>();
            services.AddScoped<ISliderService, SliderManager>();
            services.AddScoped<ISettingService, SettingManager>();
            services.AddScoped<ISubscriptionService, SubscriptionManager>();
            services.AddScoped<IProductTagService, ProductTagManager>();
            services.AddScoped<IProductImageService, ProductImageManager>();
            services.AddScoped<IBasketItemService, BasketItemManager>();
            services.AddScoped<ITagService, TagManager>();
            services.AddScoped<ICloudinaryService, CloudinaryManager>();
            services.AddScoped<IAccountService, AccountManager>();
            services.AddScoped<IBasketService, BasketManager>();
            services.AddIdentity<AppUser,IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 3;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;

                options.User.RequireUniqueEmail = true;



            })
                .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            return services;
        }
    }
}
