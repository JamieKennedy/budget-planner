using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Models
{
    public class ExpenseCategory
    {
        [Key]
        public Guid ExpenseCategoryId { get; set; }
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public string Name { get; set; }
        [StringLength(maximumLength: 6, MinimumLength = 6)]
        public string ColourHex { get; set; }

        public User User { get; set; }
    }
}
