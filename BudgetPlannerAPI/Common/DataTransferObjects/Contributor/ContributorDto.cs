using Common.DataTransferObjects.Base;

namespace Common.DataTransferObjects.Contributor
{
    public class ContributorDto : DtoBase
    {
        public Guid ContributorId { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ColourHex { get; set; } = string.Empty;

    }
}
