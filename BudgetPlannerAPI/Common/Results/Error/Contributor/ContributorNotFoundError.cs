using Common.Results.Error.Base;

namespace Common.Results.Error.Contributor
{
    public class ContributorNotFoundError : NotFoundError
    {
        public ContributorNotFoundError(Guid contributorId) : base($"No contributor found with contributor Id: {contributorId}")
        {
        }
    }
}
