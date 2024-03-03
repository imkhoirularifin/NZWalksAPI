using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.Dto;

namespace NZWalksAPI.Repositories.Interfaces
{
    public interface IWalkRepository
    {
        Task<IEnumerable<Walk?>> GetWalks(
            string? searchQuery,
            string? sortBy,
            bool asc,
            int page,
            int pageSize
        );
        Task<Walk?> GetWalk(Guid id);
        Task<(Walk? walk, string? errorMessage)> PutWalk(Guid id, UpdateWalkDto walkDto);
        Task<(ResponseWalkDto? response, string? errorMessage)> PostWalk(CreateWalkDto walkDto);
        Task<Walk?> DeleteWalk(Guid id);
    }
}
