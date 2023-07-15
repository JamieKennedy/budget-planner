using Common.DataTransferObjects.Base;

namespace Common.DataTransferObjects.Contributor
{
    public class UpdateContributorDto : UpdateDtoBase
    {
        public string Name { get; set; } = null;
        public string ColourHex { get; set; } = null;
    }
}
