using MiniMvcProject.Application.ViewModels.AppUserViewModels;
using MiniMvcProject.Domain.Entities;

namespace MiniMvcProject.Application.Services.Abstractions
{
    public interface IUserService
    {
        Task<List<AppUserViewModel>> GetAppUsersAsync();
        Task<bool> AssignRoleAsync(string id, string role);
        Task<AppUserRoleChangeViewModel> GetRoleChangeViewModelAsync(AppUser user);
        Task<bool?> ChangeStatus(AppUserStatusChangeViewModel vm);
    }
}
