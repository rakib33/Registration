using System.ComponentModel.DataAnnotations;

namespace AccountAPI.DTOs
{
    public class PinSetupDto
    {
        [Required]
        public string ICNumber { get; set; }

        [Required]
        [StringLength(6, MinimumLength = 6)]
        public string Pin { get; set; }

        [Required]
        [StringLength(6, MinimumLength = 6)]
        public string ConfirmPin { get; set; }
    }
}
