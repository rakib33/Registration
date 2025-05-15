using System.ComponentModel.DataAnnotations;

namespace AccountAPI.DTOs
{
    public class PrivacyPolicyDto
    {
        [Required]
        public string ICNumber { get; set; }

        [Required]
        public bool Agreed { get; set; }
    }
}
