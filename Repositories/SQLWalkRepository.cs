using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.Dto;
using NZWalksAPI.Repositories.Interfaces;

namespace NZWalksAPI.Repositories
{
    public class SQLWalkRepository(NZWalksDbContext context, IMapper mapper) : IWalkRepository
    {
        private readonly NZWalksDbContext context = context;
        private readonly IMapper mapper = mapper;

        public async Task<Walk?> DeleteWalk(Guid id)
        {
            var walk = await context.Walks.FindAsync(id);
            if (walk == null)
            {
                return null;
            }

            context.Walks.Remove(walk);
            await context.SaveChangesAsync();

            return walk;
        }

        public async Task<Walk?> GetWalk(Guid id)
        {
            var walk = await context
                .Walks.Include("Difficulty")
                .Include("Region")
                .FirstOrDefaultAsync(e => e.Id == id);

            if (walk == null)
            {
                return null;
            }

            return walk;
        }

        public async Task<IEnumerable<Walk?>> GetWalks(
            string? searchQuery,
            string? sortBy,
            bool asc
        )
        {
            var walks = context.Walks.Include("Difficulty").Include("Region").AsQueryable();

            // Filtering
            if (!string.IsNullOrEmpty(searchQuery))
            {
                walks = walks.Where(x => x.Name.Contains(searchQuery));
            }

            // Sorting
            if (!string.IsNullOrEmpty(sortBy))
            {
                // sort by name
                if (sortBy.Equals("name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = asc ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                }

                // sort by length
                if (sortBy.Equals("length", StringComparison.OrdinalIgnoreCase))
                {
                    walks = asc
                        ? walks.OrderBy(x => x.LengthInKm)
                        : walks.OrderByDescending(x => x.LengthInKm);
                }
            }

            return await walks.ToListAsync();
        }

        public async Task<ResponseWalkDto?> PostWalk(CreateWalkDto walkDto)
        {
            // Check if walk already exist
            var isExist = await context.Walks.AnyAsync(e => e.Name == walkDto.Name);
            if (isExist)
            {
                return null;
            }

            var difficulty = context.Difficulties.FirstOrDefault(e => e.Id == walkDto.DifficultyId);

            if (difficulty == null)
            {
                return null;
            }

            var region = context.Regions.FirstOrDefault(e => e.Id == walkDto.RegionId);

            if (region == null)
            {
                return null;
            }

            var walk = mapper.Map<Walk>(walkDto);

            await context.Walks.AddAsync(walk);
            await context.SaveChangesAsync();

            var resWalk = mapper.Map<ResponseWalkDto>(walk);
            resWalk.Difficult = difficulty.Name;
            resWalk.Region = region.Name;

            return resWalk;
        }

        public async Task<Walk?> PutWalk(Guid id, CreateWalkDto walkDto)
        {
            var walk = await context.Walks.FindAsync(id);
            if (walk == null)
            {
                return null;
            }

            // Map walkDto into Walk Domain
            walk.Name = walkDto.Name;
            walk.Description = walkDto.Description;
            walk.LengthInKm = walkDto.LengthInKm;
            walk.WalkImageUrl = walkDto.WalkImageUrl;
            walk.DifficultyId = walkDto.DifficultyId;
            walk.RegionId = walkDto.RegionId;

            await context.SaveChangesAsync();

            return walk;
        }
    }
}
