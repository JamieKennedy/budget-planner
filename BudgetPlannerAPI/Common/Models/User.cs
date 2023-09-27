using Microsoft.AspNetCore.Identity;

namespace Common.Models
{
    public class User : IdentityUser<Guid>
    {
        public List<Income> Incomes { get; set; } = new List<Income>();
    }
}
