namespace MiniMvcProject.Application.ViewModels.AppUserViewModels
{
	public class ForgetPasswordViewModel
	{
		public string Email { get; set; } = null!;
		public string? ResetLink { get; set; }
	}
}
