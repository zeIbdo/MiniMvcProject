using MiniMvcProject.Application.ViewModels.Generic;

namespace MiniMvcProject.Application.ViewModels.ServiceViewModels
{
    public class ServiceCreateViewModel : IViewModel
    {
        public int Id { get; set; }
        public required string  IconUrl { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
    }
}
