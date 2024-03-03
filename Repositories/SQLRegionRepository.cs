using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.Dto;
using NZWalksAPI.Repositories.Interfaces;

namespace NZWalksAPI.Repositories
{
    public class SQLRegionRepository(
        NZWalksDbContext context,
        IMapper mapper,
        ICloudinaryRepository cloudinaryRepository
    ) : IRegionRepository
    {
        private readonly ICloudinaryRepository cloudinaryRepository = cloudinaryRepository;
        private readonly NZWalksDbContext context = context;
        private readonly IMapper mapper = mapper;

        public async Task<Region?> DeleteRegion(Guid id)
        {
            var region = await context.Regions.FindAsync(id);
            if (region == null)
            {
                return null;
            }

            context.Regions.Remove(region);
            await context.SaveChangesAsync();

            return region;
        }

        public async Task<Region?> GetRegion(Guid id)
        {
            var region = await context.Regions.FindAsync(id);
            if (region == null)
            {
                return null;
            }

            return region;
        }

        public async Task<IEnumerable<Region?>> GetRegions()
        {
            var regions = await context.Regions.ToListAsync();
            if (regions == null)
            {
                return [];
            }

            return regions;
        }

        public async Task<(Region? region, string? errorMessage)> PostRegion(
            CreateRegionDto regionDto
        )
        {
            // Check if region already exist
            var isExsist = await context.Regions.AnyAsync(e =>
                e.Name == regionDto.Name || e.Code == regionDto.Code
            );
            if (isExsist)
            {
                return (null, "Region already exist");
            }

            var imageUrl = await cloudinaryRepository.UploadImage(regionDto.RegionImage);

            if (imageUrl == null)
            {
                return (null, "Failed uploading image");
            }

            // Using AutoMapper to map regionDto into region
            var region = mapper.Map<Region>(regionDto);
            region.RegionImageUrl = imageUrl;

            await context.Regions.AddAsync(region);
            await context.SaveChangesAsync();

            return (region, null);
        }

        public async Task<(Region?, string? errorMessage)> PutRegion(
            Guid id,
            UpdateRegionDto regionDto
        )
        {
            var region = await context.Regions.FindAsync(id);
            if (region == null)
            {
                return (null, "Region not found");
            }

            if (regionDto.RegionImage != null)
            {
                var imageUrl = await cloudinaryRepository.UploadImage(regionDto.RegionImage);

                if (imageUrl == null)
                {
                    return (null, "Failed uploading image");
                }

                region.RegionImageUrl = imageUrl;
            }

            region.Code = regionDto.Code;
            region.Name = regionDto.Name;

            // In this case, we can't use automapper to map object besause it create new object, so instead of update object that already tracked, it just do nothing

            await context.SaveChangesAsync();

            return (region, null);
        }
    }
}
