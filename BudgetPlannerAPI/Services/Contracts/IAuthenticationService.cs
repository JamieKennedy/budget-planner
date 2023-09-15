using Common.DataTransferObjects.Authentication;
using Common.DataTransferObjects.Token;

using Microsoft.AspNetCore.Identity;

namespace Services.Contracts
{
    public interface IAuthenticationService
    {
        Task<bool> AuthenticateUser(UserAuthenticationDto userAuthenticationDto);
        TokenDto CreateToken();
        Task<TokenDto> RefreshToken(RefreshTokenDto refreshTokenDto, bool trackChanges = false);
        Task<string> GeneratePasswordResetToken(string emailAddress);
        Task<IdentityResult> ResetPassword(string emailAddress, string resetToken, string newPassword);
    }
}
