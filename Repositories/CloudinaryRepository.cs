using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using NZWalksAPI.Repositories.Interfaces;

namespace NZWalksAPI.Repositories
{
    public class CloudinaryRepository(IConfiguration configuration) : ICloudinaryRepository
    {
        private readonly Cloudinary cloudinary = new(configuration["Cloudinary:Url"]);

        async Task<string?> ICloudinaryRepository.UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return null;
            }

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                UseFilename = true,
                UniqueFilename = false,
                Overwrite = true,
            };

            var uploadResult = await cloudinary.UploadAsync(uploadParams);

            if (uploadResult.Error != null)
            {
                return null;
            }

            return uploadResult.Url.ToString();
        }
    }
}
