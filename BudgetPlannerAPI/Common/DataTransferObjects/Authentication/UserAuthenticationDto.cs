using System.ComponentModel.DataAnnotations;

namespace Common.DataTransferObjects.Authentication
{
    public class UserAuthenticationDto
    {
        [Required]
        public required string Email { get; init; }
        [Required]
        public required string Password { get; init; }
        public bool KeepLoggedIn { get; init; } = false;
    }
}
