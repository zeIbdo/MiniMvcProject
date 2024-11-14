using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MiniMvcProject.Application.Services.Abstractions;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;

namespace MiniMvcProject.Application.Services.Implementations
{
    public class CloudinaryManager : ICloudinaryService
    {
        private readonly IConfiguration _configuration;
        private readonly Cloudinary _cloudinary;
        public CloudinaryManager(IConfiguration configuration)
        {
            _configuration = configuration;
            var myAccount = new Account
            {
                ApiKey = _configuration["CloudinarySettings:APIKey"],
                ApiSecret = _configuration["CloudinarySettings:APISecret"],
                Cloud = _configuration["CloudinarySettings:CloudName"]
            };
            _cloudinary = new Cloudinary(myAccount);
            _cloudinary.Api.Secure = true;
        }

        public async Task<string> ImageCreateAsync(IFormFile file)
        {
            string fileName = string.Concat(Guid.NewGuid(), file.FileName.Substring(file.FileName.LastIndexOf('.')));
            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(fileName, stream),
                };
                uploadResult = await _cloudinary.UploadAsync(uploadParams);

            }
            string url = uploadResult.SecureUrl.ToString();

            return url;
        }

        public  bool ImageDelete(string fileName)
        {
            var url = GetPublicIdFromUrl(fileName);
            var deletionParams = new DeletionParams(url)
            {
                ResourceType = ResourceType.Image
            };
            DeletionResult deletionResult = _cloudinary.Destroy(deletionParams);
            if (deletionResult.Result == "ok")
                return true;

            else 
                return false;
        }


        static string GetPublicIdFromUrl(string imageUrl)
        {
            Uri uri = new Uri(imageUrl);

            string[] segments = uri.AbsolutePath.Split('/');

            string fileName = segments[^1];

            string publicId = fileName.Substring(0, fileName.LastIndexOf('.'));

            return publicId;
        }
    }
}
