using Common.Models.Base;

namespace Common.DataTransferObjects.User
{
    public class UserDto : ModifiableBase
    {
        public string UserName { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public bool EmailConfirmed { get; init; }
        public string PhoneNumber { get; init; } = string.Empty;
        public bool PhoneNumberConfirmed { get; init; }
        public bool TwoFactorEnabled { get; init; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; init; }
        public DateTime? LastLogin { get; set; }
        public IList<string> Roles { get; init; } = new List<string>();
    }

}
