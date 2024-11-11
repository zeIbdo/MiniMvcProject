using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiniMvcProject.DAL.DataContext;
using MiniMvcProject.Persistance.Repositories.Abstractions;
using MiniMvcProject.Persistance.Repositories.Implementations;

namespace MiniMvcProject.Persistance
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Default"),
                b => b.MigrationsAssembly("QuickCom.Persistence"));
            });

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IBasketItemRepository, BasketItemRepository>();
            services.AddScoped<IProductImageRepository, ProductImageRepository>();
            services.AddScoped<IProductTagRepository, ProductTagRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<ISettingRepository, SettingRepository>();
            services.AddScoped<ISliderRepository, SliderRepository>();
            services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
            services.AddScoped<ITagRepository, TagRepository>();

            return services;
        }
    }
}
