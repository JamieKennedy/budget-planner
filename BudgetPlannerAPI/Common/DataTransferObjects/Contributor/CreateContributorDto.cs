using Common.DataTransferObjects.Base;

namespace Common.DataTransferObjects.Contributor
{
    public class CreateContributorDto : CreateDtoBase
    {
        public string Name { get; set; }
        public string ColourHex { get; set; }
    }
}
