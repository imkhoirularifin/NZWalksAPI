using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.Dto;

namespace NZWalksAPI.Repositories.Interfaces
{
    public interface IWalkRepository
    {
        Task<IEnumerable<Walk?>> GetWalks(string? searchQuery, string? sortBy, bool asc);
        Task<Walk?> GetWalk(Guid id);
        Task<Walk?> PutWalk(Guid id, CreateWalkDto walkDto);
        Task<ResponseWalkDto?> PostWalk(CreateWalkDto walkDto);
        Task<Walk?> DeleteWalk(Guid id);
    }
}
