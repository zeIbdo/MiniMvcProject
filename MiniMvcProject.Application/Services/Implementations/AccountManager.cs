using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MiniMvcProject.Application.Services.Abstractions;
using MiniMvcProject.Application.ViewModels;
using MiniMvcProject.Application.ViewModels.AppUserViewModels;
using MiniMvcProject.Domain.Entities;
using MiniMvcProject.Domain.Enums;
using Newtonsoft.Json;

namespace MiniMvcProject.Application.Services.Implementations
{
    public class AccountManager : IAccountService
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountManager(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> LoginAsync(LoginViewModel vm, ModelStateDictionary modelState)
        {

            if (_httpContextAccessor.HttpContext.User.Identity?.IsAuthenticated ?? true)
            {
                modelState.AddModelError("","User already signed");
                return false;
            }
            if (!modelState.IsValid)
                return false;
             
            var user = await _userManager.FindByEmailAsync(vm.UsernameOrEmail);

            if (user is null)
                user = await _userManager.FindByNameAsync(vm.UsernameOrEmail);

            if(user is null)
            {
                modelState.AddModelError("", "Username or password is incorrect");
                return false;
            }

            if(user.IsDisabled)
                return false;

            var result = await _signInManager.PasswordSignInAsync(user, vm.Password, vm.RememberMe,true);
            if (!result.Succeeded)
            {
                modelState.AddModelError("", "Username or password is incorrect");
                return false;
            }

            return true;
        }
       
        public async Task<bool> RegisterAsync(RegisterViewModel vm, ModelStateDictionary modelState)
        {

            if (_httpContextAccessor.HttpContext.User.Identity?.IsAuthenticated ?? true)
            {
                modelState.AddModelError("", "User already signed");
                return false;
            }
            if (!modelState.IsValid)
                return false;

            if (vm.ConfirmPassword != vm.Password)
            {
                modelState.AddModelError("", "Passwords don't match");
                return false;
            }

            var newUser = _mapper.Map<AppUser>(vm);
            newUser.IsDisabled = false;

            var result = await _userManager.CreateAsync(newUser, vm.Password);
            await _userManager.AddToRoleAsync(newUser,RoleType.Member.ToString());

            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    modelState.AddModelError("", item.Description);
                }
                return false;
            }

            return true;
        }

        public async Task<bool> SignOutAsync()
        {
            
            await _signInManager.SignOutAsync();

            return true;
        }
    }
}
