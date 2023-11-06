using AutoMapper;

using Common.DataTransferObjects.Income;
using Common.Models;
using Common.Results.Error.Account;
using Common.Results.Error.Base;
using Common.Results.Error.Income;
using Common.Results.Error.User;

using FluentResults;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

using Repository.Contracts;

using Services.Contracts;

namespace Services
{
    public class IncomeService : IIncomeService
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repositoryManager;
        private readonly UserManager<User> _userManager;

        public IncomeService(IConfiguration configuration, IMapper mapper, IRepositoryManager repositoryManager, UserManager<User> userManager)
        {
            _configuration = configuration;
            _mapper = mapper;
            _repositoryManager = repositoryManager;
            _userManager = userManager;
        }

        public async Task<Result<IncomeDto>> CreateIncome(Guid userId, CreateIncomeDto createIncomeDto)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null) return new UserNotFoundError(userId);

            var account = _repositoryManager.Account.SelectById(createIncomeDto.AccountId);
            if (account is null) return new AccountNotFoundError(createIncomeDto.AccountId);

            if (account.UserId != user.Id)
            {
                return new UnauthorisedError("Cannot create income for account not belonging to the user");
            }

            var income = _mapper.Map<Income>(createIncomeDto);
            income.UserId = user.Id;

            income = _repositoryManager.Income.CreateIncome(income);
            _repositoryManager.Save();

            return _mapper.Map<IncomeDto>(income);
        }

        public async Task<Result> DeleteIncome(Guid userId, Guid incomeId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null) return new UserNotFoundError(userId);

            var income = _repositoryManager.Income.SelectById(incomeId);
            if (income is null) return new IncomeNotFoundError(incomeId);

            if (income.UserId != user.Id) return new UnauthorisedError("Cannot delete income belonging to other users");

            _repositoryManager.Income.DeleteIncome(income);
            _repositoryManager.Save();

            return Result.Ok();
        }

        public async Task<Result<IncomeDto>> SelectById(Guid userId, Guid incomeId, bool trackChanges = false)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null) return new UserNotFoundError(userId);

            var income = _repositoryManager.Income.SelectById(incomeId);
            if (income is null) return new IncomeNotFoundError(incomeId);

            if (income.UserId != user.Id)
            {
                return new UnauthorisedError("Cannot access income for other users");
            }

            return _mapper.Map<IncomeDto>(income);
        }

        public async Task<Result<List<IncomeDto>>> SelectByUserId(Guid userId, bool trackChanges = false)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null) return new UserNotFoundError(userId);

            var incomes = _repositoryManager.Income.SelectByUserId(userId, trackChanges);

            return _mapper.Map<List<IncomeDto>>(incomes) ?? new List<IncomeDto>();
        }

        public async Task<Result<IncomeDto>> UpdateIncome(Guid userId, Guid incomeId, UpdateIncomeDto updateIncomeDto)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null) return new UserNotFoundError(userId);

            var income = _repositoryManager.Income.SelectById(incomeId);
            if (income is null) return new IncomeNotFoundError(incomeId);

            if (income.UserId != user.Id) return new UnauthorisedError("Cannot update income belonging to other users");

            income.Name = updateIncomeDto.Name ?? income.Name;
            income.Amount = updateIncomeDto.Amount ?? income.Amount;


            _repositoryManager.Income.UpdateIncome(income);
            _repositoryManager.Save();

            return _mapper.Map<IncomeDto>(income);

        }
    }
}
