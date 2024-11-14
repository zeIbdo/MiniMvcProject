using Microsoft.AspNetCore.Mvc.Rendering;

namespace MiniMvcProject.Application.ViewModels.AppUserViewModels
{
    public class AppUserRoleChangeViewModel
    {
        public required string Id { get; set; }
        public List<SelectListItem> Roles { get; set;} = new();
        public string Role {  get; set; }
    }
}
