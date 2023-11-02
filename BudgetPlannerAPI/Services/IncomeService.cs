using AutoMapper;

using Common.DataTransferObjects.Income;
using Common.Exceptions.Account;
using Common.Exceptions.Base;
using Common.Exceptions.User;
using Common.Models;

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

        public async Task<IncomeDto> CreateIncome(Guid userId, CreateIncomeDto createIncomeDto)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString()) ?? throw new UserNotFoundException(userId);

            var account = _repositoryManager.Account.SelectById(createIncomeDto.AccountId) ?? throw new AccountNotFoundException(createIncomeDto.AccountId);

            if (account.UserId != user.Id)
            {
                throw new UnauthorisedException("Cannot create income for account not belonging to the user");
            }

            var income = _mapper.Map<Income>(createIncomeDto);
            income.UserId = user.Id;

            income = _repositoryManager.Income.CreateIncome(income);
            _repositoryManager.Save();

            return _mapper.Map<IncomeDto>(income);
        }

        public async Task DeleteIncome(Guid userId, Guid incomeId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString()) ?? throw new UserNotFoundException(userId);

            var income = _repositoryManager.Income.SelectById(incomeId) ?? throw new IncomeNotFoundException(incomeId);

            if (income.UserId != user.Id) throw new UnauthorisedException("Cannot delete income belonging to other users");

            _repositoryManager.Income.DeleteIncome(income);
            _repositoryManager.Save();
        }

        public async Task<IncomeDto> SelectById(Guid userId, Guid incomeId, bool trackChanges = false)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString()) ?? throw new UserNotFoundException(userId);

            var income = _repositoryManager.Income.SelectById(incomeId) ?? throw new IncomeNotFoundException(incomeId);

            if (income.UserId != user.Id)
            {
                throw new UnauthorisedException("Cannot access income for other users");
            }

            return _mapper.Map<IncomeDto>(income);
        }

        public async Task<IEnumerable<IncomeDto>> SelectByUserId(Guid userId, bool trackChanges = false)
        {
            var _ = await _userManager.FindByIdAsync(userId.ToString()) ?? throw new UserNotFoundException(userId);

            var incomes = _repositoryManager.Income.SelectByUserId(userId, trackChanges);

            return _mapper.Map<IEnumerable<IncomeDto>>(incomes) ?? Enumerable.Empty<IncomeDto>();
        }

        public async Task<IncomeDto> UpdateIncome(Guid userId, Guid incomeId, UpdateIncomeDto updateIncomeDto)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString()) ?? throw new UserNotFoundException(userId);

            var income = _repositoryManager.Income.SelectById(incomeId) ?? throw new IncomeNotFoundException(incomeId);

            if (income.UserId != user.Id) throw new UnauthorisedException("Cannot update income belonging to other users");

            income.Name = updateIncomeDto.Name ?? income.Name;
            income.Amount = updateIncomeDto.Amount ?? income.Amount;


            _repositoryManager.Income.UpdateIncome(income);
            _repositoryManager.Save();

            return _mapper.Map<IncomeDto>(income);

        }
    }
}
