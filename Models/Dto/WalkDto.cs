namespace NZWalksAPI.Models.Dto
{
    public class CreateWalkDto
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }
    }

    public class ResponseWalkDto
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        public required string Difficult { get; set; }
        public required string Region { get; set; }
    }
}
