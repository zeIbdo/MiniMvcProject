using MiniMvcProject.Application.ViewModels.Generic;

namespace MiniMvcProject.Application.ViewModels.TagViewModels
{
    public class TagUpdateViewModel : IViewModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
    }
}
