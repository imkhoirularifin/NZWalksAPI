namespace NZWalksAPI.Models.Dto
{
    public class UpdateRegionDto
    {
        public required string Code { get; set; }
        public required string Name { get; set; }
        public IFormFile? RegionImage { get; set; }
    }
}
