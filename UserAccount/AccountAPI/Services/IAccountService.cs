using AccountAPI.DTOs;

namespace AccountAPI.Services
{
    public interface IAccountService
    {
        Task<ServiceResult> RegisterAsync(RegistrationDto registrationDto);
        Task<ServiceResult> VerifyAccountAsync(VerificationDto verificationDto);
        Task<ServiceResult> AgreeToPrivacyPolicyAsync(PrivacyPolicyDto privacyPolicyDto);
        Task<ServiceResult> SetupPinAsync(PinSetupDto pinSetupDto);
        Task<ServiceResult> SetupBiometricAsync(BiometricDto biometricDto);

        Task<ServiceResult> ResendVerificationCodeAsync(string icNumber);
    }
}
