using System.ComponentModel.DataAnnotations;

namespace NZWalksAPI.Models.Domain
{
    public class Walk
    {
        public Guid Id { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public required string Description { get; set; }

        [Required]
        [MaxLength(100)]
        public required double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }

        // Relationship
        [Required]
        public Guid DifficultyId { get; set; }

        [Required]
        public Guid RegionId { get; set; }

        // Navigation properties
        public required Difficulty Difficulty { get; set; }
        public required Region Region { get; set; }
    }
}
