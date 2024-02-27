using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.Dto;

namespace NZWalksAPI.Repositories.Interfaces
{
    public interface IDifficultyRepository
    {
        Task<IEnumerable<Difficulty?>> GetDifficulties();
        Task<Difficulty?> GetDifficulty(Guid id);
        Task<Difficulty?> PutDifficulty(Guid id, DifficultyDto difficultyDto);

        Task<Difficulty?> PostDifficulty(DifficultyDto difficultyDto);
        Task<Difficulty?> DeleteDifficulty(Guid id);
    }
}
