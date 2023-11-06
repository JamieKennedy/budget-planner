using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Common.Models.Base;

namespace Common.Models
{
    public class Account : ModifiableBase
    {
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        [StringLength(maximumLength: 6, MinimumLength = 6)]
        public string? ColourHex { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Balance { get; set; }
        public User? User { get; set; }
    }
}
