using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

using AutoMapper;

using Common.DataTransferObjects.Authentication;
using Common.DataTransferObjects.Token;
using Common.Exceptions.Configuration;
using Common.Exceptions.User;
using Common.Models;

using LoggerService.Interfaces;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using Repository.Contracts;

using Services.Contracts;

namespace Services
{
    internal class AuthenticationService : IAuthenticationService
    {
        private readonly ILoggerManager _loggerManager;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IRepositoryManager _repositoryManager;

        public AuthenticationService(ILoggerManager loggerManager, IMapper mapper, UserManager<User> userManager, IConfiguration configuration, IRepositoryManager repositoryManager)
        {
            _loggerManager = loggerManager;
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
            _repositoryManager = repositoryManager;
        }

        public async Task<bool> AuthenticateUser(UserAuthenticationDto userAuthenticationDto)
        {
            var user = await _userManager.FindByEmailAsync(userAuthenticationDto.Email) ?? throw new UserNotFoundException(userAuthenticationDto.Email);

            bool authResult = await _userManager.CheckPasswordAsync(user, userAuthenticationDto.Password);

            if (!authResult)
            {
                _loggerManager.LogWarning($"Authentication failed for email: {userAuthenticationDto.Email}");
                await _userManager.AccessFailedAsync(user);
            }
            else
            {
                user.LastLogin = DateTime.Now;
                await _userManager.UpdateAsync(user);
            }


            return authResult;
        }

        public async Task<TokenDto> CreateToken(string emailAddress)
        {
            var user = await _userManager.FindByEmailAsync(emailAddress) ?? throw new UserNotFoundException(emailAddress);

            return await CreateToken(user);
        }

        public async Task<TokenDto> CreateToken(User user)
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims(user);
            var token = GetToken(signingCredentials, claims);

            var refreshToken = CreateRefreshToken(user);

            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

            return new TokenDto()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        public async Task<TokenDto> RefreshToken(RefreshTokenDto refreshTokenDto, bool trackChanges = false)
        {
            var token = _repositoryManager.Tokens.SelectByRefreshToken(refreshTokenDto.RefreshToken, trackChanges) ?? throw new RefreshTokenInvalidException("Invalid refresh token");

            if (!token.Active)
            {
                // Inactive token attempted to be used, invalidate all refresh tokens
                // to maintain security
                var activeTokens = _repositoryManager.Tokens.SelectActiveByUserId(token.UserId, true).ToList();

                activeTokens.ForEach(activeToken => activeToken.Active = false);
                _repositoryManager.Save();

                // Return unauthorized response
                throw new RefreshTokenInvalidException("Inactive token");
            }

            var user = await _userManager.FindByIdAsync(token.UserId.ToString()) ?? throw new UserNotFoundException(token.UserId);

            // Create new token
            return await CreateToken(user);
        }

        private JwtSecurityToken GetToken(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");

            var tokenOptions = new JwtSecurityToken(
                issuer: jwtSettings["ValidIssuer"],
                audience: jwtSettings["ValidAudience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["AccessTokenExpiryMinutes"])),
                signingCredentials: signingCredentials
            );

            return tokenOptions;

        }

        private SigningCredentials GetSigningCredentials()
        {
            var jwtSecret = _configuration["JwtSettings:Secret"];

            if (string.IsNullOrEmpty(jwtSecret))
            {
                throw new ConfigurationItemNotFoundException("JwtSettings:Secret");
            }

            var key = Encoding.UTF8.GetBytes(jwtSecret);
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims(User user)
        {
            if (user == null)
            {
                return new List<Claim>();
            }



            var claims = new List<Claim>
            {
                new ("Id", user.Id.ToString()),
                new ("Email", user.Email ?? string.Empty)

            };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        private string CreateRefreshToken(User user)
        {
            var activeTokens = _repositoryManager.Tokens.SelectActiveByUserId(user.Id, true).ToList();

            if (activeTokens.Any())
            {
                // User has active tokens, invalidate all before creating a new one
                activeTokens.ToList().ForEach(token =>
                {
                    token.Active = false;
                });
            }

            var newToken = GenerateRefreshToken();

            // Repeatedly attempt to retrieve the token using this string. If it exists
            // create a new one. Unlikely to get conflicts but here just in case
            while (_repositoryManager.Tokens.SelectByRefreshToken(newToken, false) != null)
            {
                newToken = GenerateRefreshToken();
            }


            // Get refresh token expiry from config
            var expiryDaysString = _configuration.GetSection("JwtSettings")["RefreshTokenExpiryDays"];

            if (string.IsNullOrEmpty(expiryDaysString))
            {
                throw new ConfigurationItemNotFoundException("JwtSettings:RefreshTokenExpiryDays");
            }

            var expiryDays = int.Parse(expiryDaysString);

            var tokenToInsert = new Token()
            {
                UserId = user.Id,
                RefreshToken = newToken,
                Expires = DateTime.Now.AddDays(expiryDays),
                Active = true
            };

            // Add the token to the db
            _repositoryManager.Tokens.CreateToken(tokenToInsert);
            _repositoryManager.Save();

            return newToken;
        }

        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);

            return Convert.ToBase64String(randomNumber);
        }

        public async Task<string> GeneratePasswordResetToken(string emailAddress)
        {
            var user = await _userManager.FindByEmailAsync(emailAddress) ?? throw new UserNotFoundException(emailAddress);

            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<IdentityResult> ResetPassword(string emailAddress, string resetToken, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(emailAddress) ?? throw new UserNotFoundException(emailAddress);

            return await _userManager.ResetPasswordAsync(user, resetToken, newPassword);
        }
    }
}
