using MiniMvcProject.Application.ViewModels.Generic;

namespace MiniMvcProject.Application.ViewModels.TagViewModels
{
    public class TagCreateViewModel : IViewModel
    {
        public required string Name { get; set; }
    }
}
