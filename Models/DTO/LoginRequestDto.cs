using System.ComponentModel.DataAnnotations;

namespace NZWalksAPI.Models.DTO
{
    public class LoginRequestDto
    {
        // requied [Required]
        [DataType(DataType.EmailAddress)]
        public required string UserName { get; set; }

        // [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}