using AccountAPI.Data;
using AccountAPI.DTOs;
using AccountAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AccountAPI.Services
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _context;
        private readonly IVerificationService _verificationService;

        public AccountService(ApplicationDbContext context, IVerificationService verificationService)
        {
            _context = context;
            _verificationService = verificationService;
        }

        public async Task<ServiceResult> RegisterAsync(RegistrationDto registrationDto)
        {
            // Check if account already exists
            var existingAccount = await _context.Accounts
                .FirstOrDefaultAsync(a => a.ICNumber == registrationDto.ICNumber);

            if (existingAccount != null)
            {
                return ServiceResult.Fail("Account already exists");
            }

            // Create new account
            var account = new Account
            {
                CustomerName = registrationDto.CustomerName,
                ICNumber = registrationDto.ICNumber,
                MobileNumber = registrationDto.MobileNumber,
                EmailAddress = registrationDto.EmailAddress
            };

            // Generate and send verification code
            var verificationResult = await _verificationService.SendVerificationCodeAsync(account);
            if (!verificationResult.Success)
            {
                return verificationResult;
            }

            account.VerificationCode = verificationResult.Data.ToString();
            account.VerificationCodeExpiry = DateTime.UtcNow.AddMinutes(10);

            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();

            return ServiceResult.Ok("Verification code sent to your mobile and email");
        }

        public async Task<ServiceResult> VerifyAccountAsync(VerificationDto verificationDto)
        {
            var account = await _context.Accounts
                .FirstOrDefaultAsync(a => a.ICNumber == verificationDto.ICNumber);

            if (account == null)
            {
                return ServiceResult.Fail("Account not found");
            }

            if (account.IsVerified)
            {
                return ServiceResult.Fail("Account is already verified");
            }

            if (account.VerificationCode != verificationDto.VerificationCode)
            {
                return ServiceResult.Fail("Invalid verification code");
            }

            if (account.VerificationCodeExpiry < DateTime.UtcNow)
            {
                return ServiceResult.Fail("Verification code has expired");
            }

            account.IsVerified = true;
            account.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return ServiceResult.Ok("Account verified successfully");
        }

        public async Task<ServiceResult> AgreeToPrivacyPolicyAsync(PrivacyPolicyDto privacyPolicyDto)
        {
            var account = await _context.Accounts
                .FirstOrDefaultAsync(a => a.ICNumber == privacyPolicyDto.ICNumber);

            if (account == null)
            {
                return ServiceResult.Fail("Account not found");
            }

            if (!account.IsVerified)
            {
                return ServiceResult.Fail("Account is not verified");
            }

            account.PrivacyPolicyAgreed = privacyPolicyDto.Agreed;
            account.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return ServiceResult.Ok("Privacy policy agreement updated");
        }

        public async Task<ServiceResult> SetupPinAsync(PinSetupDto pinSetupDto)
        {
            var account = await _context.Accounts
                .FirstOrDefaultAsync(a => a.ICNumber == pinSetupDto.ICNumber);

            if (account == null)
            {
                return ServiceResult.Fail("Account not found");
            }

            if (!account.IsVerified)
            {
                return ServiceResult.Fail("Account is not verified");
            }

            if (!account.PrivacyPolicyAgreed)
            {
                return ServiceResult.Fail("Privacy policy not agreed");
            }

            if (pinSetupDto.Pin != pinSetupDto.ConfirmPin)
            {
                return ServiceResult.Fail("PIN and Confirm PIN do not match");
            }

            // In real application, hash the PIN before storing
           // account.PinHash = BCrypt.Net.BCrypt.HashPassword(pinSetupDto.Pin);
            account.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return ServiceResult.Ok("PIN setup successfully");
        }

        public async Task<ServiceResult> SetupBiometricAsync(BiometricDto biometricDto)
        {
            var account = await _context.Accounts
                .FirstOrDefaultAsync(a => a.ICNumber == biometricDto.ICNumber);

            if (account == null)
            {
                return ServiceResult.Fail("Account not found");
            }

            if (!account.IsVerified)
            {
                return ServiceResult.Fail("Account is not verified");
            }

            if (string.IsNullOrEmpty(account.PinHash))
            {
                return ServiceResult.Fail("PIN not setup");
            }

            // In real application, encrypt and store fingerprint data securely
            account.IsBiometricSet = true;
            account.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return ServiceResult.Ok("Biometric setup successfully");
        }

        public async Task<ServiceResult> ResendVerificationCodeAsync(string icNumber)
        {
            var account = await _context.Accounts
                .FirstOrDefaultAsync(a => a.ICNumber == icNumber);

            if (account == null)
            {
                return ServiceResult.Fail("Account not found");
            }

            // Reuse the verification service to send new code
            var verificationResult = await _verificationService.SendVerificationCodeAsync(account);
            if (!verificationResult.Success)
            {
                return verificationResult;
            }

            // Update the account with new verification code
            account.VerificationCode = verificationResult.Data.ToString();
            account.VerificationCodeExpiry = DateTime.UtcNow.AddMinutes(10);
            account.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return ServiceResult.Ok("Verification code resent successfully");
        }
    }
}
