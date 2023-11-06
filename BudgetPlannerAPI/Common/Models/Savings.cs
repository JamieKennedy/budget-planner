using System.ComponentModel.DataAnnotations.Schema;

using Common.Models.Base;

namespace Common.Models
{
    public class Savings : ModifiableBase
    {
        [ForeignKey(nameof(Models.User))]
        public Guid UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Goal { get; set; }
        public DateTime? GoalDate { get; set; }

        public User? User { get; set; }
        public IEnumerable<SavingsBalance> SavingsBalances { get; set; } = new List<SavingsBalance>();
    }
}
