using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.Dto;
using NZWalksAPI.Repositories.Interfaces;

namespace NZWalksAPI.Repositories
{
    public class SQLDifficultyRepository(NZWalksDbContext context) : IDifficultyRepository
    {
        private readonly NZWalksDbContext context = context;

        public async Task<Difficulty?> DeleteDifficulty(Guid id)
        {
            var difficulty = await context.Difficulties.FindAsync(id);
            if (difficulty == null)
            {
                return null;
            }

            context.Difficulties.Remove(difficulty);
            await context.SaveChangesAsync();

            return difficulty;
        }

        public async Task<IEnumerable<Difficulty?>> GetDifficulties()
        {
            var difficulties = await context.Difficulties.ToListAsync();
            if (difficulties == null)
            {
                return [];
            }

            return difficulties;
        }

        public async Task<Difficulty?> GetDifficulty(Guid id)
        {
            var difficulty = await context.Difficulties.FindAsync(id);
            if (difficulty == null)
            {
                return null;
            }

            return difficulty;
        }

        public async Task<Difficulty?> PostDifficulty(DifficultyDto difficultyDto)
        {
            // Check if difficulty already exist
            var isExsist = await context.Difficulties.AnyAsync(e => e.Name == difficultyDto.Name);
            if (isExsist)
            {
                return null;
            }

            var difficulty = new Difficulty { Name = difficultyDto.Name };

            await context.Difficulties.AddAsync(difficulty);
            await context.SaveChangesAsync();

            return difficulty;
        }

        public async Task<Difficulty?> PutDifficulty(Guid id, DifficultyDto difficultyDto)
        {
            var difficulty = await context.Difficulties.FindAsync(id);
            if (difficulty == null)
            {
                return null;
            }

            difficulty.Name = difficultyDto.Name;

            await context.SaveChangesAsync();

            return difficulty;
        }
    }
}
