using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MiniMvcProject.Application.Services.Abstractions;
using MiniMvcProject.Application.ViewModels.AppUserViewModels;
using MiniMvcProject.Domain.Entities;
using MiniMvcProject.Domain.Enums;

namespace MiniMvcProject.ADMIN.Controllers;
[AutoValidateAntiforgeryToken]
[Authorize(Roles = "Admin")]
public class UserController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IUserService _userService;

    public UserController(UserManager<AppUser> userManager, IUserService userService)
    {
        _userManager = userManager;
        _userService = userService;
    }

    public async Task<IActionResult> Index()
    {
        var users =await _userService.GetAppUsersAsync();

        return View(users);
    }

    public async Task<IActionResult> AssignRole(string id)
    {
        var user = _userManager.Users.FirstOrDefault(u => u.Id == id);
        
        if (user == null) return BadRequest();
        return View(await _userService.GetRoleChangeViewModelAsync(user));
    }

    [HttpPost]
    public async Task<IActionResult> AssignRole(AppUserRoleChangeViewModel vm)
    {
        if(!ModelState.IsValid) 
            return View(vm);

        var result = await _userService.AssignRoleAsync(vm.Id, vm.Role);
        if (result == false) return BadRequest();

       
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> ToggleUser(string id)
    {
        var user = await _userManager.FindByIdAsync(id);

        if (user == null) return NotFound();

        return View(new AppUserStatusChangeViewModel { AppUserId=user.Id,IsDisabled=user.IsDisabled });
    }

    [HttpPost]
    public async Task<IActionResult> ToggleUser(AppUserStatusChangeViewModel vm)
    {
        if (!ModelState.IsValid)
            return View(vm);
        var result = await _userService.ChangeStatus(vm);
        if(result == null) return NotFound();
        //if(result==false)
        //{
        //    ModelState.AddModelError("", "invalid date");
        //    return View(vm);
        //}

        return RedirectToAction(nameof(Index));
    }
}
