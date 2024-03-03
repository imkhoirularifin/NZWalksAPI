using System.ComponentModel.DataAnnotations;

namespace NZWalksAPI.Models.Dto
{
    public class RegisterRequestDto
    {
        [Required]
        public required string Username { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        public required string EmailAddress { get; set; }

        [Required, DataType(DataType.Password)]
        public required string Password { get; set; }

        [Required]
        public required string Role { get; set; }
    }
}
