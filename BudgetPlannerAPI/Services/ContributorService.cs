using AutoMapper;

using Common.DataTransferObjects.Contributor;
using Common.Models;
using Common.Results.Error.Contributor;
using Common.Results.Error.User;

using FluentResults;

using Microsoft.AspNetCore.Identity;

using Microsoft.Extensions.Configuration;

using Repository.Contracts;

using Services.Contracts;

namespace Services
{
    public class ContributorService : IContributorService
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repositoryManager;
        private readonly UserManager<User> _userManager;

        public ContributorService(IConfiguration configuration, IMapper mapper, IRepositoryManager repositoryManager, UserManager<User> userManager)
        {
            _configuration = configuration;
            _mapper = mapper;
            _repositoryManager = repositoryManager;
            _userManager = userManager;
        }


        public async Task<Result<ContributorDto>> CreateContributor(Guid userId, CreateContributorDto createContributorDto)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null) return new UserNotFoundError(userId);

            var contributorModel = _mapper.Map<Contributor>(createContributorDto);
            contributorModel.UserId = user.Id;

            var contributor = _repositoryManager.Contributor.CreateContributor(contributorModel);
            _repositoryManager.Save();

            var dto = _mapper.Map<ContributorDto>(contributor);

            return dto;
        }

        public async Task<Result> DeleteContributor(Guid userId, Guid contributorId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null) return new UserNotFoundError(userId);

            var contributor = _repositoryManager.Contributor.SelectById(contributorId);
            if (contributor is null) return new ContributorNotFoundError(contributorId);

            _repositoryManager.Contributor.DeleteContributor(contributor);
            _repositoryManager.Save();

            return Result.Ok();
        }

        public async Task<Result<ContributorDto>> SelectById(Guid userId, Guid contributorId, bool trackChanges = false)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null) return new UserNotFoundError(userId);

            var contributor = _repositoryManager.Contributor.SelectById(contributorId, trackChanges);
            if (contributor is null) return new ContributorNotFoundError(contributorId);

            var dto = _mapper.Map<ContributorDto>(contributor);
            return dto;
        }

        public async Task<Result<List<ContributorDto>>> SelectByUserId(Guid userId, bool trackChanges = false)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null) return new UserNotFoundError(userId);

            var contributors = _repositoryManager.Contributor.SelectByUserId(user.Id);

            var dto = _mapper.Map<List<ContributorDto>>(contributors) ?? new List<ContributorDto>();

            return dto;
        }

        public async Task<Result<ContributorDto>> UpdateContributor(Guid userId, Guid contributorId, UpdateContributorDto updateContributorDto)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null) return new UserNotFoundError(userId);

            var contributor = _repositoryManager.Contributor.SelectById(contributorId);
            if (contributor is null) return new ContributorNotFoundError(contributorId);

            contributor.Name = updateContributorDto.Name is null ? contributor.Name : updateContributorDto.Name;
            contributor.ColourHex = updateContributorDto.ColourHex is null ? contributor.ColourHex : updateContributorDto.ColourHex;

            var updatedContributor = _repositoryManager.Contributor.UpdateContributor(contributor);
            _repositoryManager.Save();

            var dto = _mapper.Map<ContributorDto>(updatedContributor);

            return dto;


        }
    }
}
