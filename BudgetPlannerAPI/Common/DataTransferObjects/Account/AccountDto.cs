using Common.DataTransferObjects.Base;

namespace Common.DataTransferObjects.Account
{
    public class AccountDto : DtoBase
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? ColourHex { get; set; }
        public decimal Balance { get; set; }
    }
}
