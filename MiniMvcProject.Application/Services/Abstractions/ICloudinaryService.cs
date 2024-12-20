﻿using Microsoft.AspNetCore.Http;

namespace MiniMvcProject.Application.Services.Abstractions
{
    public interface ICloudinaryService
    {
        Task<string> ImageCreateAsync(IFormFile file);
        bool ImageDelete(string fileName);
    }
}
