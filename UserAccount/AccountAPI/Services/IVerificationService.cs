using AccountAPI.Models;

namespace AccountAPI.Services
{
    public interface IVerificationService
    {
        Task<ServiceResult> SendVerificationCodeAsync(Account account);
    }
}
