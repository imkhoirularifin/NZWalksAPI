namespace NZWalksAPI.Repositories.Interfaces
{
    public interface ICloudinaryRepository
    {
        Task<string?> UploadImage(IFormFile file);
    }
}
