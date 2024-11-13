using Microsoft.AspNetCore.Mvc;
using MiniMvcProject.Application.Services.Abstractions;
using MiniMvcProject.Application.ViewModels.AppUserViewModels;

namespace MiniMvcProject.MVC.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            var result = await _accountService.RegisterAsync(vm, ModelState);

            if (result != true)
            {
                return View(vm);
            }

            return RedirectToAction("Index","Home");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            var result = await _accountService.LoginAsync(vm, ModelState);
            
            if (result != true)
            {
                return View(vm);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
