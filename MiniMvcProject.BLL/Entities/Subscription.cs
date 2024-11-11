namespace MiniMvcProject.Domain.Entities
{
    public class Subscription : BaseEntity
    {
        public required string Email { get; set; }
        public bool IsActive { get; set; }

    }
}
