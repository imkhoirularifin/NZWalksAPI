using System.ComponentModel.DataAnnotations;

namespace NZWalksAPI.Models.Dto
{
    public class LoginRequestDto
    {
        [Required, DataType(DataType.EmailAddress)]
        public required string EmailAddress { get; set; }

        [Required, DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}
