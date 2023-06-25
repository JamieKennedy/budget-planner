

using System.ComponentModel.DataAnnotations;

namespace Common.Models.User.Dto
{
    public class CreateUserDto
    {
        [Required]
        public string ClerkId { get; set; } = string.Empty;
    }
}
