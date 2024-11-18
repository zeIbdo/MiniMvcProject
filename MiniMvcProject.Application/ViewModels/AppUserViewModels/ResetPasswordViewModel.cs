using System.ComponentModel.DataAnnotations;

namespace MiniMvcProject.Application.ViewModels.AppUserViewModels
{
	public class ResetPasswordViewModel
	{
		[DataType(DataType.Password)]
		public string Password { get; set; } = null!;
		[DataType(DataType.Password), Compare(nameof(Password))]
		public string ConfirmPassword { get; set; } = null!;
	}
}
