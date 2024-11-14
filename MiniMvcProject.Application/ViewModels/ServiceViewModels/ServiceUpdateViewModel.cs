using Microsoft.AspNetCore.Http;
using MiniMvcProject.Application.ViewModels.Generic;

namespace MiniMvcProject.Application.ViewModels.ServiceViewModels
{
    public class ServiceUpdateViewModel : IViewModel
    {
        public int Id { get; set; }
        public  IFormFile? IconImage { get; set; }
        public  string? IconUrl { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
    }
}
