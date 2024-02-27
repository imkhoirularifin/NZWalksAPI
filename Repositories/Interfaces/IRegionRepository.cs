using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.Dto;

namespace NZWalksAPI.Repositories.Interfaces
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region?>> GetRegions();
        Task<Region?> GetRegion(Guid id);
        Task<Region?> PutRegion(Guid id, RegionDto regionDto);

        Task<Region?> PostRegion(RegionDto regionDto);
        Task<Region?> DeleteRegion(Guid id);
    }
}
