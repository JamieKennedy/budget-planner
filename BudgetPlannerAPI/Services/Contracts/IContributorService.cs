using Common.DataTransferObjects.Contributor;

namespace Services.Contracts
{
    public interface IContributorService
    {
        Task<ContributorDto> CreateContributor(Guid userId, CreateContributorDto createContributorDto);
        Task<ContributorDto> UpdateContributor(Guid userId, Guid contributorId, UpdateContributorDto updateContributorDto);
        void DeleteContributor(Guid userId, Guid contributorId);
        Task<ContributorDto> SelectById(Guid userId, Guid contributorId, bool trackChanges = false);
        Task<IEnumerable<ContributorDto>> SelectByUserId(Guid userId, bool trackChanges = false);
    }
}
