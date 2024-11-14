using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MiniMvcProject.Application.Services.Abstractions;
using MiniMvcProject.Application.ViewModels.AppUserViewModels;
using MiniMvcProject.Domain.Entities;
using MiniMvcProject.Domain.Enums;

namespace MiniMvcProject.ADMIN.Controllers;

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

    public IActionResult Index()
    {
        var users = _userService.GetAppUsers();

        return View(users);
    }

    public async Task<IActionResult> AssignRole(string id)
    {
        var user = _userManager.Users.FirstOrDefault(u => u.Id == id);

        if (user == null) return NotFound();
        var roles = new List<string>();
        roles = Enum.GetValues(typeof(RoleType))
           .Cast<RoleType>()
           .Select(r => r.ToString())
           .ToList();
        var roleItems = roles.Select(r => new SelectListItem
        {
            Text = r,
            Value = r
        }).ToList();
        var userRole =await _userManager.GetRolesAsync(user);
        return View(new AppUserRoleChangeViewModel { Id = id, Roles = roleItems });
    }

    [HttpPost]
    public async Task<IActionResult> AssignRole(AppUserRoleChangeViewModel vm)
    {
        var result = await _userService.AssignRoleAsync(vm.Id, vm.Role);

        return RedirectToAction(nameof(Index));
    }
}
