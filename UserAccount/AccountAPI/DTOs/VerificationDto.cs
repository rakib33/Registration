using System.ComponentModel.DataAnnotations;

namespace AccountAPI.DTOs
{
    public class VerificationDto
    {
        [Required]
        public string ICNumber { get; set; }

        [Required]
        [StringLength(4)]
        public string VerificationCode { get; set; }
    }
}
