using System.ComponentModel.DataAnnotations;

namespace AccountAPI.DTOs
{
    public class ResendVerificationDto
    {
        [Required]
        [StringLength(20)]
        public string ICNumber { get; set; }
    }
}
