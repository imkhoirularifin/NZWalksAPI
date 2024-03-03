using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.Dto;

namespace NZWalksAPI.Repositories.Interfaces
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region?>> GetRegions();
        Task<Region?> GetRegion(Guid id);
        Task<(Region?, string? errorMessage)> PutRegion(Guid id, UpdateRegionDto regionDto);

        Task<(Region? region, string? errorMessage)> PostRegion(CreateRegionDto regionDto);
        Task<Region?> DeleteRegion(Guid id);
    }
}
