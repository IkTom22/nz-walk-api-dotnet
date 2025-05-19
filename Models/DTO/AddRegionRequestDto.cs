using System.ComponentModel.DataAnnotations;

namespace NZWalksAPI.Models.DTO
{
    public class AddRegionRequestDto
    {
        // [Required]
        [MinLength(3, ErrorMessage = "Code has to be minimum of 3 characers")]
        [MaxLength(3, ErrorMessage = "Code has to be maximum of 3 characters")]
        public required string Code { get; set; }
        // [Required]
        [MaxLength(100, ErrorMessage = "Name has to be maximum of 100 characters")]
        public required string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}