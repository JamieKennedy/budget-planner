using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Common.Models.Base;

namespace Common.Models
{
    public class Token : ModelBase
    {

        [ForeignKey(nameof(User))]
        public required Guid UserId { get; set; }

        [Required]
        public required string RefreshToken { get; set; }

        [Required]
        public DateTime Expires { get; set; }

        [Required]
        [DefaultValue(false)]
        public bool Active { get; set; }

        public User? User { get; set; }
    }
}
