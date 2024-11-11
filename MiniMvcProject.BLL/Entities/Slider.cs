﻿namespace MiniMvcProject.Domain.Entities
{
    public class Slider : BaseEntity
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string ImageUrl { get; set; }
        public decimal Price { get; set; }
    }
}
