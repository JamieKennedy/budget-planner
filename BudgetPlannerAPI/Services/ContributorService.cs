using AutoMapper;

using Common.DataTransferObjects.Contributor;
using Common.Exceptions.Contributor;
using Common.Exceptions.User;
using Common.Models;

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


        public async Task<ContributorDto> CreateContributor(Guid userId, CreateContributorDto createContributorDto)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString()) ?? throw new UserNotFoundException(userId);

            var contributorModel = _mapper.Map<Contributor>(createContributorDto);
            contributorModel.UserId = user.Id;

            var contributor = _repositoryManager.Contributor.CreateContributor(contributorModel);
            _repositoryManager.Save();

            return _mapper.Map<ContributorDto>(contributor);
        }

        public async void DeleteContributor(Guid userId, Guid contributorId)
        {
            var _ = await _userManager.FindByIdAsync(userId.ToString()) ?? throw new UserNotFoundException(userId);

            var contributor = _repositoryManager.Contributor.SelectById(contributorId) ?? throw new ContributorNotFoundException(contributorId);

            _repositoryManager.Contributor.DeleteContributor(contributor);
            _repositoryManager.Save();
        }

        public async Task<ContributorDto> SelectById(Guid userId, Guid contributorId, bool trackChanges = false)
        {
            var _ = await _userManager.FindByIdAsync(userId.ToString()) ?? throw new UserNotFoundException(userId);

            var contributor = _repositoryManager.Contributor.SelectById(contributorId, trackChanges) ?? throw new ContributorNotFoundException(contributorId);

            return _mapper.Map<ContributorDto>(contributor);
        }

        public async Task<IEnumerable<ContributorDto>> SelectByUserId(Guid userId, bool trackChanges = false)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString()) ?? throw new UserNotFoundException(userId);

            var contributors = _repositoryManager.Contributor.SelectByUserId(user.Id);

            return _mapper.Map<IEnumerable<ContributorDto>>(contributors) ?? Enumerable.Empty<ContributorDto>();
        }

        public async Task<ContributorDto> UpdateContributor(Guid userId, Guid contributorId, UpdateContributorDto updateContributorDto)
        {
            var _ = await _userManager.FindByIdAsync(userId.ToString()) ?? throw new UserNotFoundException(userId);

            var contributor = _repositoryManager.Contributor.SelectById(contributorId) ?? throw new ContributorNotFoundException(contributorId);

            contributor.Name = updateContributorDto.Name is null ? contributor.Name : updateContributorDto.Name;
            contributor.ColourHex = updateContributorDto.ColourHex is null ? contributor.ColourHex : updateContributorDto.ColourHex;

            var updatedContributor = _repositoryManager.Contributor.UpdateContributor(contributor);
            _repositoryManager.Save();

            return _mapper.Map<ContributorDto>(updatedContributor);


        }
    }
}
