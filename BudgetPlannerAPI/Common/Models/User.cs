using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.AspNetCore.Identity;

namespace Common.Models
{
    public class User : IdentityUser<Guid>
    {
        public DateTime? LastLogin { get; set; }
        public List<Income> Incomes { get; set; } = new List<Income>();
        [NotMapped]
        public IList<string> Roles { get; set; } = new List<string>();
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }


    }
}
