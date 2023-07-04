using Common.DataTransferObjects.Authentication;
using Common.DataTransferObjects.Token;

namespace Services.Contracts
{
    public interface IAuthenticationService
    {
        Task<bool> AuthenticateUser(UserAuthenticationDto userAuthenticationDto);
        TokenDto CreateToken();
        Task<TokenDto> RefreshToken(RefreshTokenDto refreshTokenDto, bool trackChanges = false);
    }
}
