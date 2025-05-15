using System.ComponentModel.DataAnnotations;

namespace AccountAPI.DTOs
{
    public class RegistrationDto
    {
        [Required]
        [StringLength(100)]
        public string CustomerName { get; set; }

        [Required]
        [StringLength(20)]
        public string ICNumber { get; set; }

        [Required]
        [StringLength(15)]
        public string MobileNumber { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string EmailAddress { get; set; }
    }
}
