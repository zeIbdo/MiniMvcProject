using MiniMvcProject.Domain.Entities;

namespace MiniMvcProject.Application.ViewModels.Generic
{
    public class ResultViewModel<T> 
    {
        public bool Success { get; set; }=true;
        public string Message { get; set; } = "";
        public T? Data { get; set; }
    }
}
