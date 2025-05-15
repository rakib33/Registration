using System.ComponentModel.DataAnnotations;

namespace AccountAPI.DTOs
{
    public class BiometricDto
    {
        [Required]
        public string ICNumber { get; set; }

        [Required]
        public string FingerprintData { get; set; }
    }
}
