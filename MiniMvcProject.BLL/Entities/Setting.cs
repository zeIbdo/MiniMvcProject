namespace MiniMvcProject.Domain.Entities
{
    public class Setting:BaseEntity
    {
        public required string Key { get; set; }
        public required string Value { get; set; }
    }
}
