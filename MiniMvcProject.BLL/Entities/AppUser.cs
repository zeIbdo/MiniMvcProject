using Microsoft.AspNetCore.Identity;

namespace MiniMvcProject.Domain.Entities
{
    public class AppUser:IdentityUser
    {
        public bool IsDisabled { get; set; }
    }
}
