using MiniMvcProject.Application.ViewModels.Generic;

namespace MiniMvcProject.Application.ViewModels.TagViewModels
{
    public class TagViewModel:IViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
