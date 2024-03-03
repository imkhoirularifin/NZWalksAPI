namespace NZWalksAPI.Models.Dto
{
    public class CreateRegionDto
    {
        public required string Code { get; set; }
        public required string Name { get; set; }
        public required IFormFile RegionImage { get; set; }
    }
}
