using Common.DataTransferObjects.Base;

namespace Common.DataTransferObjects.Contributor
{
    public class CreateContributorDto : CreateDtoBase
    {
        public string Name { get; set; } = string.Empty;
        public string ColourHex { get; set; } = string.Empty;
    }
}
