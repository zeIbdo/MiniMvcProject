using MiniMvcProject.Application.UI.ViewModels;
using MiniMvcProject.Application.ViewModels.BasketItemViewModels;

namespace MiniMvcProject.Application.Services.Abstractions
{
    public interface IBasketService
    {
        Task<List<BasketItemViewModel>> AddProductsToCookieBasketAsync(int productId);
        Task AddProductsToDbBasketAsync(int productId);
        Task AddToBasketAsync(int productId);
        Task<List<BasketItemViewModel>> DecreaseProductsToCookieBasketAsync(int productId);
        Task DecreaseProductsToDbBasketAsync(int productId);

        Task DecreaseBasketItemAsync(int productId);
         Task<bool> DeleteFromDB(int productId);
         Task<List<BasketItemViewModel>> DeleteFromCookie(int productId);

    }
}
