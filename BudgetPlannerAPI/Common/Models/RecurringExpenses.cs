using System.ComponentModel.DataAnnotations.Schema;

using Common.Models.Base;

namespace Common.Models
{
    public class RecurringExpenses : ModifiableBase
    {
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int DayOfMonth { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public Decimal Amount { get; set; }
        public IEnumerable<Contributor> Contributors { get; set; } = Enumerable.Empty<Contributor>();
        public ExpenseCategory? Category { get; set; }
        public User? User { get; set; }
    }
}
