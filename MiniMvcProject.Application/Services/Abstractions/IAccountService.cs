using Microsoft.AspNetCore.Mvc.ModelBinding;
using MiniMvcProject.Application.ViewModels;
using MiniMvcProject.Application.ViewModels.AppUserViewModels;

namespace MiniMvcProject.Application.Services.Abstractions
{
    public interface IAccountService
    {
        Task<bool> RegisterAsync(RegisterViewModel vm, ModelStateDictionary modelState);
        Task<bool> LoginAsync(LoginViewModel vm, ModelStateDictionary modelState);
        Task<bool> SignOutAsync();
    }
}
