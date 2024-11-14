using MiniMvcProject.Application.ViewModels.AppUserViewModels;

namespace MiniMvcProject.Application.Services.Abstractions
{
    public interface IUserService
    {
        List<AppUserViewModel> GetAppUsers();
        Task<bool> AssignRoleAsync(string id, string role);
    }
}
