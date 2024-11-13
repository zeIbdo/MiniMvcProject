using MiniMvcProject.Application.UI.ViewModels;
using MiniMvcProject.Application.ViewModels.BasketItemViewModels;

namespace MiniMvcProject.Application.Services.Abstractions
{
    public interface IBasketService
    {
        Task AddProductsToCookieBasketAsync(int productId);
        Task AddProductsToDbBasketAsync(int productId);
        Task AddToBasketAsync(int productId);
    }
}
