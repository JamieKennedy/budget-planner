using Common.DataTransferObjects.Base;

namespace Common.DataTransferObjects.Account
{
    public class CreateAccountDto : CreateDtoBase
    {
        public string Name { get; set; } = string.Empty;
        public string? ColourHex { get; set; }
        public decimal Balance { get; set; }
    }
}
