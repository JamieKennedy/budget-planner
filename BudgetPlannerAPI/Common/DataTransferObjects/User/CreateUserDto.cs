

using System.ComponentModel.DataAnnotations;

namespace Common.DataTransferObjects.User
{
    public class CreateUserDto
    {
        [Required]
        public string ClerkId { get; set; } = string.Empty;
    }
}
