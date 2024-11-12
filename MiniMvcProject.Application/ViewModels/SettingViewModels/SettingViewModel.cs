﻿using MiniMvcProject.Application.ViewModels.Generic;

namespace MiniMvcProject.Application.ViewModels.SettingViewModels
{
    public class SettingViewModel:IViewModel
    {
        public int Id { get; set; }
        public string? Key { get; set; }
        public string? Value { get; set; }
    }
}