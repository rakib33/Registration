using System.ComponentModel.DataAnnotations;

namespace AccountAPI.Models
{
    public class Account
    {
        [Key]
        public int Id { get; set; }

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

        public bool IsVerified { get; set; } = false;
        public string VerificationCode { get; set; }
        public DateTime? VerificationCodeExpiry { get; set; }

        public bool PrivacyPolicyAgreed { get; set; } = false;
        public string PinHash { get; set; }
        public bool IsBiometricSet { get; set; } = false;

        public byte[] BiometricData { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
