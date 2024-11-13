using MiniMvcProject.Application.UI.ViewModels;

namespace MiniMvcProject.Application.Services.Abstractions
{
    public interface IBasketService
    {
        Task<string> AddProductsToCookieBasketAsync(int productId);
        Task<string> AddProductsToDbBasketAsync(int productId);
        List<BasketViewModel> GetBasket();
    }
}
