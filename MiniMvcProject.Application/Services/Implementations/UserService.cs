using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MiniMvcProject.Application.Services.Abstractions;
using MiniMvcProject.Application.ViewModels.AppUserViewModels;
using MiniMvcProject.Domain.Entities;

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
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null) return false;
            var usersRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, usersRoles);
            if (!await _userManager.IsInRoleAsync(user, role))
            {
                var result = await _userManager.AddToRoleAsync(user, role);
            }
            return true;
        }

        public List<AppUserViewModel> GetAppUsers()
        {
            var users = _userManager.Users.ToList();
            return _mapper.Map<List<AppUserViewModel>>(users);
        }
    }
}
