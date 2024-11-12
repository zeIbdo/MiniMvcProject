﻿using MiniMvcProject.Application.ViewModels.Generic;

namespace MiniMvcProject.Application.ViewModels.SubscriptionViewModels
{
    public class SubscriptionUpdateViewModel : IViewModel
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public bool IsActive { get; set; }
    }
}