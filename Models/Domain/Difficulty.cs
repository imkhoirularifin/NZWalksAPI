using System.ComponentModel.DataAnnotations;

namespace NZWalksAPI.Models.Domain
{
    public class Difficulty
    {
        public Guid Id { get; set; }

        [Required]
        public required string Name { get; set; }
    }
}
