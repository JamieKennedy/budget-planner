using Common.DataTransferObjects.Base;

namespace Common.DataTransferObjects.Account
{
    public class UpdateAccountDto : UpdateDtoBase
    {
        public string? Name { get; set; } = null;
        public string? ColourHex { get; set; } = null;
        public decimal? Balance { get; set; } = null;
    }
}
