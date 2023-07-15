using Common.Exceptions.Base;

namespace Common.Exceptions.Contributor
{
    public class ContributorNotFoundException : NotFoundException
    {
        public ContributorNotFoundException(Guid contributorId) : base($"No contributor found with contributor Id: {contributorId}")
        {
        }
    }
}
