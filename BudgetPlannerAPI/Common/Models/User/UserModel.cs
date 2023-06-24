using System.ComponentModel.DataAnnotations;

namespace Common.Models.User
{
    public class UserModel
    {
        [Key]
        public long UserId { get; set; }
        public string? ClerkId { get; set; }
    }
}
