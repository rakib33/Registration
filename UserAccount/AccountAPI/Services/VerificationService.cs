using AccountAPI.Models;

namespace AccountAPI.Services
{
    public class VerificationService : IVerificationService
    {
        // In a real application, inject email and SMS services here
        public async Task<ServiceResult> SendVerificationCodeAsync(Account account)
        {
            try
            {
                // Generate random 4-digit code
                var random = new Random();
                var verificationCode = random.Next(1000, 9999).ToString();

                // In real application:
                // 1. Send SMS to account.MobileNumber with verificationCode
                // 2. Send Email to account.EmailAddress with verificationCode
                // For now, we'll just simulate this

                Console.WriteLine($"Verification code {verificationCode} sent to:");
                Console.WriteLine($"Mobile: {account.MobileNumber}");
                Console.WriteLine($"Email: {account.EmailAddress}");

                return ServiceResult.Ok(verificationCode);
            }
            catch (Exception ex)
            {
                return ServiceResult.Fail($"Failed to send verification code: {ex.Message}");
            }
        }
    }
}
