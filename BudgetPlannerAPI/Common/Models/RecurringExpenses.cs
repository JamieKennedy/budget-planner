using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Models
{
    public class RecurringExpenses : ModelBase
    {
        [Key]
        public Guid RecurringExpenseId { get; set; }
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public int DayOfMonth { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public Decimal Amount { get; set; }
        public IEnumerable<Contributer> Contributors { get; set; } = Enumerable.Empty<Contributer>();
        public ExpenseCategory Category { get; set; }
        public User User { get; set; }
    }
}
