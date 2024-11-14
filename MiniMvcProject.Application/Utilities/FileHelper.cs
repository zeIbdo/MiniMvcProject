using Microsoft.AspNetCore.Http;

namespace MiniMvcProject.Application.Utilities
{
    public static class FileHelper
    {
        public static bool CheckType(this IFormFile file, string type = "image")
        {
            return file.ContentType.Contains(type);
        }
    }
}
