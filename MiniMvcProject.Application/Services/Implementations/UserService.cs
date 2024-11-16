using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiniMvcProject.Application.Services.Abstractions;
using MiniMvcProject.Application.ViewModels.AppUserViewModels;
using MiniMvcProject.Domain.Entities;
using MiniMvcProject.Domain.Enums;

namespace MiniMvcProject.Application.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<bool> AssignRoleAsync(string id, string role)
        {
            if(role==null)
                return false;
            if(role.ToUpper()=="ADMIN")
                return false;
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
            var usersRoles = await _userManager.GetRolesAsync(user!);
            await _userManager.RemoveFromRolesAsync(user!, usersRoles);
            if (!await _userManager.IsInRoleAsync(user!, role))
            {
                var result = await _userManager.AddToRoleAsync(user!, role);
            }
            return true;
        }

        public async Task<bool?> ChangeStatus(AppUserStatusChangeViewModel vm)
        {
            var user = await _userManager.FindByIdAsync(vm.AppUserId);

            if (user == null) return null;
            user.LockoutEnabled = vm.OnLockout;
            await _userManager.UpdateSecurityStampAsync(user);

            await _userManager.UpdateAsync(user);
            return true;
        }

        public async Task<List<AppUserViewModel>> GetAppUsersAsync()
        {
            var users = _userManager.Users.ToList();
            var nonAdminUsers = new List<AppUser>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                if (!roles.Contains("Admin"))
                {
                    nonAdminUsers.Add(user);
                }
            }
            return _mapper.Map<List<AppUserViewModel>>(nonAdminUsers);
        }

        public async Task<AppUserRoleChangeViewModel> GetRoleChangeViewModelAsync(AppUser user)
        {
            var roles = new List<string>();
            roles = Enum.GetValues(typeof(RoleType))
               .Cast<RoleType>()
               .Where(r => r.ToString() != RoleType.Admin.ToString())
               .Select(r => r.ToString())
               .ToList();
            var roleItems = roles.Select(r => new SelectListItem
            {
                Text = r,
                Value = r
            }).ToList();
            var userRole = await _userManager.GetRolesAsync(user);
            return new AppUserRoleChangeViewModel { Id = user.Id, Roles = roleItems };
        }
    }
}
