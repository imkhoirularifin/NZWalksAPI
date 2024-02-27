namespace NZWalksAPI.Models.Dto
{
    public class RegionDto
    {
        public required string Code { get; set; }
        public required string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
