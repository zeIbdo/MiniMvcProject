namespace MiniMvcProject.Domain.Entities
{
    public class Service:BaseEntity
    {
        public required string IconUrl { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
    }
}
