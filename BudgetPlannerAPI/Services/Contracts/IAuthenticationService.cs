using Common.DataTransferObjects.Authentication;
using Common.DataTransferObjects.Token;
using Common.Models;

using FluentResults;

namespace Services.Contracts
{
    public interface IAuthenticationService
    {
        Task<Result> AuthenticateUser(UserAuthenticationDto userAuthenticationDto);
        Task<Result<TokenDto>> CreateToken(string emailAddress);
        Task<Result<TokenDto>> CreateToken(User user);
        Task<Result<TokenDto>> RefreshToken(RefreshTokenDto refreshTokenDto, bool trackChanges = false);
        Task<Result<string>> GeneratePasswordResetToken(string emailAddress);
        Task<Result> ResetPassword(string emailAddress, string resetToken, string newPassword);
    }
}
