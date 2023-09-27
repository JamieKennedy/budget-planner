using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Common.Constants.Enums;

namespace Common.Models
{
    public class Income : ModelBase
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        [ForeignKey(nameof(Account))]
        public Guid AccountId { get; set; }
        public string Name { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        public EOccurrence Occurrence { get; set; }
        public EOccurrsOn OccurrsOn { get; set; }
        /// <remarks>
        /// Should only have a value if <c>OccurrsOn</c> is Custom
        /// </remarks>
        public int? CustomOccurrsOn { get; set; }

        public virtual required User User { get; set; }
        public virtual required Account Account { get; set; }


    }
}
