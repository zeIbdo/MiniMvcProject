using MiniMvcProject.Application.ViewModels.Generic;

namespace MiniMvcProject.Application.ViewModels.ServiceViewModels
{
    public class ServiceViewModel:IViewModel
    {
        public int Id { get; set; }
        public string? IconUrl { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
    }
}
