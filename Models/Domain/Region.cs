using System.ComponentModel.DataAnnotations;

namespace NZWalksAPI.Models.Domain
{
    public class Region
    {
        public Guid Id { get; set; }

        [Required]
        public required string Code { get; set; }

        [Required]
        public required string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
