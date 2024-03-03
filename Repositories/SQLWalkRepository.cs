using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.Dto;
using NZWalksAPI.Repositories.Interfaces;

namespace NZWalksAPI.Repositories
{
    public class SQLWalkRepository(
        NZWalksDbContext context,
        IMapper mapper,
        ICloudinaryRepository cloudinaryRepository
    ) : IWalkRepository
    {
        private readonly NZWalksDbContext context = context;
        private readonly IMapper mapper = mapper;
        private readonly ICloudinaryRepository cloudinaryRepository = cloudinaryRepository;

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
            bool asc,
            int page,
            int pageSize
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

            // Pagination
            var skip = (page - 1) * pageSize;

            return await walks.Skip(skip).Take(pageSize).ToListAsync();
        }

        public async Task<(ResponseWalkDto? response, string? errorMessage)> PostWalk(
            CreateWalkDto walkDto
        )
        {
            // Check if walk already exist
            var isExist = await context.Walks.AnyAsync(e => e.Name == walkDto.Name);
            if (isExist)
            {
                return (null, "Walk with that name is already exist");
            }

            // Check if difficulty exist
            var difficulty = context.Difficulties.FirstOrDefault(e => e.Id == walkDto.DifficultyId);

            if (difficulty == null)
            {
                return (null, "Difficulty not found");
            }

            // Check if region exist
            var region = context.Regions.FirstOrDefault(e => e.Id == walkDto.RegionId);

            if (region == null)
            {
                return (null, "Region not found");
            }

            // Upload walk image
            var imageUrl = await cloudinaryRepository.UploadImage(walkDto.WalkImage);
            if (imageUrl == null)
            {
                return (null, "Error uploading image");
            }

            var walk = mapper.Map<Walk>(walkDto);
            walk.WalkImageUrl = imageUrl;

            await context.Walks.AddAsync(walk);
            await context.SaveChangesAsync();

            var resWalk = mapper.Map<ResponseWalkDto>(walk);
            resWalk.Difficult = difficulty.Name;
            resWalk.Region = region.Name;

            return (resWalk, null);
        }

        public async Task<(Walk? walk, string? errorMessage)> PutWalk(
            Guid id,
            UpdateWalkDto walkDto
        )
        {
            var walk = await context.Walks.FindAsync(id);
            if (walk == null)
            {
                return (null, "Walk not found");
            }

            if (walkDto.WalkImage != null)
            {
                var imageUrl = await cloudinaryRepository.UploadImage(walkDto.WalkImage);

                if (imageUrl == null)
                {
                    return (null, "Error uploading image");
                }

                walk.WalkImageUrl = imageUrl;
            }

            // Map walkDto into Walk Domain
            walk.Name = walkDto.Name;
            walk.Description = walkDto.Description;
            walk.LengthInKm = walkDto.LengthInKm;
            walk.DifficultyId = walkDto.DifficultyId;
            walk.RegionId = walkDto.RegionId;

            await context.SaveChangesAsync();

            return (walk, null);
        }
    }
}
