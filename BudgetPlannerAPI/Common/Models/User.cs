using System.ComponentModel.DataAnnotations;

namespace Common.Models
{
    public class User
    {
        [Key]
        public long UserId { get; set; }
        public string? ClerkId { get; set; }
    }
}
