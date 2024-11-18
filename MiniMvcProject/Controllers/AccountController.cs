using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MiniMvcProject.Application.Services.Abstractions;
using MiniMvcProject.Application.ViewModels.AppUserViewModels;
using MiniMvcProject.Domain.Entities;

namespace MiniMvcProject.MVC.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        private readonly IAccountService _accountService;
        private readonly IEmailService _emailManager;

        public AccountController(IAccountService accountService, UserManager<AppUser> userManager, IEmailService emailManager)
        {
            _accountService = accountService;
            _userManager = userManager;
            _emailManager = emailManager;
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

        public async Task<IActionResult> LogOut()
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
                return NotFound();
            var result = await _accountService.SignOutAsync();


            if (result is false)
                return BadRequest();

            return RedirectToAction("Index", "Home");

        }


		[AllowAnonymous]

		public IActionResult ForgetPassword()
		{
			return View();
		}
		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel vm)
		{
			if (!ModelState.IsValid) return View(vm);
			var existingUser = await _userManager.FindByEmailAsync(vm.Email);
			if (existingUser == null)
			{
				ModelState.AddModelError("", "There is not such a user with this email");
				return View(vm);
			}
			var resetToken = await _userManager.GeneratePasswordResetTokenAsync(existingUser);
			var resetLink = Url.Action(nameof(ResetPassword), "Account", new { vm.Email, resetToken }, Request.Scheme, Request.Host.ToString());
			string subject = "Reset Password with this token";
			string body = $"<b>Password Reset Token</b><br><hr> <a href=\"{resetLink}\">Recovery Link</a> ";
			try
			{
				_emailManager.SendEmail(vm.Email, subject, body);
				Console.WriteLine("Mail sent");
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			return RedirectToAction("Index");
		}
		public IActionResult ResetPassword()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ResetPassword(ResetPasswordViewModel vm, string email, string resetToken)
		{
			if (!ModelState.IsValid)
			{
				return View(vm);
			}
			var existingUser = await _userManager.FindByEmailAsync(email);
			if (existingUser == null) return BadRequest();
			var passwordCheck = await _userManager.CheckPasswordAsync(existingUser, vm.Password);
			if (passwordCheck)
			{
				ModelState.AddModelError("", "You cannot use your previous password.");
				return View(vm);
			}
			var result = await _userManager.ResetPasswordAsync(existingUser, resetToken, vm.Password);
			if (result.Succeeded)
				return RedirectToAction(nameof(Login));

			foreach (var error in result.Errors)
			{
				ModelState.AddModelError("", error.Description);
			}
			return View(vm);
		}

	}
}
