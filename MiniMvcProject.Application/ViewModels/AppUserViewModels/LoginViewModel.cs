namespace MiniMvcProject.Application.ViewModels.AppUserViewModels
{
    public class LoginViewModel
    {
        public required string UsernameOrEmail { get; set; }
        public required string Password { get; set; }
        public bool RememberMe { get; set; } = false;
    }
}
