using MiniMvcProject.Application.ViewModels.Generic;

namespace MiniMvcProject.Application.ViewModels.AppUserViewModels
{
    public class AppUserViewModel:IViewModel
    {
        public required string Id { get; set; }  
        public string? UserName { get; set; }  
        public string? Email { get; set; }  

    }
}
