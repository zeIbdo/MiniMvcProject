using MiniMvcProject.Application.ViewModels.CategoryViewModels;
using MiniMvcProject.Domain.Entities;

namespace MiniMvcProject.Application.UI.ViewModels
{
    public class HeaderViewModel
    {
        public string? SupportNumber { get; set; }
        public List<CategoryViewModel> Categories { get; set; } = new();
    }
}
