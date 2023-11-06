using Common.DataTransferObjects.Contributor;

using FluentResults;

namespace Services.Contracts
{
    public interface IContributorService
    {
        Task<Result<ContributorDto>> CreateContributor(Guid userId, CreateContributorDto createContributorDto);
        Task<Result<ContributorDto>> UpdateContributor(Guid userId, Guid contributorId, UpdateContributorDto updateContributorDto);
        Task<Result> DeleteContributor(Guid userId, Guid contributorId);
        Task<Result<ContributorDto>> SelectById(Guid userId, Guid contributorId, bool trackChanges = false);
        Task<Result<List<ContributorDto>>> SelectByUserId(Guid userId, bool trackChanges = false);
    }
}
