namespace MiniMvcProject.Application.ViewModels.AppUserViewModels
{
    public class AppUserStatusChangeViewModel
    {
        public required string AppUserId { get; set; }
        //public DateTimeOffset LockoutEnds { get; set; }
        public bool OnLockout { get; set; }
    }
}
