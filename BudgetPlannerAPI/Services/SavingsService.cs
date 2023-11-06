using AutoMapper;

using Common.DataTransferObjects.Savings;
using Common.Models;
using Common.Results.Error.Savings;
using Common.Results.Error.User;

using FluentResults;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

using Repository.Contracts;

using Services.Contracts;

namespace Services
{
    public class SavingsService : ISavingsService
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repositoryManager;
        private readonly UserManager<User> _userManager;

        public SavingsService(IConfiguration configuration, IMapper mapper, IRepositoryManager repositoryManager, UserManager<User> userManager)
        {
            _configuration = configuration;
            _mapper = mapper;
            _repositoryManager = repositoryManager;
            _userManager = userManager;
        }

        public async Task<Result<SavingsDto>> CreateSavings(Guid userId, CreateSavingsDto createSavingsDto)
        {
            // Check user exists
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null) return new UserNotFoundError(userId);

            var savingsModel = _mapper.Map<Savings>(createSavingsDto);
            savingsModel.UserId = user.Id;

            var savings = _repositoryManager.Savings.CreateSavings(savingsModel);
            _repositoryManager.Save();

            var savingsDto = _mapper.Map<SavingsDto>(savings);

            return savingsDto;
        }

        public Result<SavingsDto> SelectById(Guid savingsId)
        {
            var savings = _repositoryManager.Savings.SelectById(savingsId);
            if (savings is null) return new SavingsNotFoundError(savingsId);

            savings.SavingsBalances = _repositoryManager.SavingsBalance.SelectBySavingsId(savings.Id);

            var savingsDto = _mapper.Map<SavingsDto>(savings);

            return savingsDto;
        }

        public async Task<Result<List<SavingsDto>>> SelectByUserId(Guid userId, bool trackChanges = false)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null) return new UserNotFoundError(userId);

            var savings = _repositoryManager.Savings.SelectByUserId(user.Id, trackChanges);

            var savingsDtos = _mapper.Map<List<SavingsDto>>(savings) ?? new List<SavingsDto>();

            return savingsDtos;
        }

        public async Task<Result> DeleteById(Guid userId, Guid savingsId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null) return new UserNotFoundError(userId);

            var savings = _repositoryManager.Savings.SelectById(savingsId);
            if (savings is null) return new SavingsNotFoundError(savingsId);

            _repositoryManager.Savings.DeleteSavings(savings);
            _repositoryManager.Save();

            return Result.Ok();
        }

        public async Task<Result<SavingsDto>> UpdateSavings(Guid userId, Guid savingsId, UpdateSavingsDto updateSavingsDto)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null) return new UserNotFoundError(userId);

            var savings = _repositoryManager.Savings.SelectById(savingsId);
            if (savings is null) return new SavingsNotFoundError(savingsId);

            // update properties if supplied
            savings.Name = updateSavingsDto.Name is null ? savings.Name : updateSavingsDto.Name;
            savings.Description = updateSavingsDto.Description is null ? savings.Description : updateSavingsDto.Description;
            savings.Goal = updateSavingsDto.Goal is null ? savings.Goal : updateSavingsDto.Goal.Value;
            savings.GoalDate = updateSavingsDto.GoalDate == DateTime.MinValue ? savings.GoalDate : updateSavingsDto.GoalDate;
            savings.LastModified = DateTime.Now;

            Savings updatedSavings = _repositoryManager.Savings.UpdateSavings(savings);
            _repositoryManager.Save();

            return _mapper.Map<SavingsDto>(updatedSavings);
        }
    }
}
