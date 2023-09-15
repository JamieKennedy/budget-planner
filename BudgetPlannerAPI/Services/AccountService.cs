using AutoMapper;

using Common.DataTransferObjects.Account;
using Common.Exceptions.Account;
using Common.Exceptions.User;
using Common.Models;

using Microsoft.AspNetCore.Identity;

using Microsoft.Extensions.Configuration;

using Repository.Contracts;

using Services.Contracts;

namespace Services
{
    public class AccountService : IAccountService
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repositoryManager;
        private readonly UserManager<User> _userManager;

        public AccountService(IConfiguration configuration, IMapper mapper, IRepositoryManager repositoryManager, UserManager<User> userManager)
        {
            _configuration = configuration;
            _mapper = mapper;
            _repositoryManager = repositoryManager;
            _userManager = userManager;
        }

        public async Task<AccountDto> CreateAccount(Guid userId, CreateAccountDto createAccountDto)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString()) ?? throw new UserNotFoundException(userId);

            var account = _mapper.Map<Account>(createAccountDto);
            account.UserId = user.Id;

            account = _repositoryManager.Account.CreateAccount(account);
            _repositoryManager.Save();

            return _mapper.Map<AccountDto>(account);
        }

        public async Task DeleteAccount(Guid userId, Guid accountId)
        {
            var _ = await _userManager.FindByIdAsync(userId.ToString()) ?? throw new UserNotFoundException(userId);

            var account = _repositoryManager.Account.SelectById(accountId) ?? throw new AccountNotFoundException(accountId);

            _repositoryManager.Account.DeleteAccount(account);
            _repositoryManager.Save();

        }

        public async Task<AccountDto> SelectById(Guid userId, Guid accountId, bool trackChanges = false)
        {
            var _ = await _userManager.FindByIdAsync(userId.ToString()) ?? throw new UserNotFoundException(userId);

            var account = _repositoryManager.Account.SelectById(accountId) ?? throw new AccountNotFoundException(accountId);

            return _mapper.Map<AccountDto>(account);
        }

        public async Task<IEnumerable<AccountDto>> SelectByUserId(Guid userId, bool trackChanges = false)
        {
            var _ = await _userManager.FindByIdAsync(userId.ToString()) ?? throw new UserNotFoundException(userId);

            var accounts = _repositoryManager.Account.SelectByUserId(userId);

            return _mapper.Map<IEnumerable<AccountDto>>(accounts) ?? Enumerable.Empty<AccountDto>();
        }

        public async Task<AccountDto> UpdateAccount(Guid userId, Guid accountId, UpdateAccountDto updateAccountDto)
        {
            var _ = await _userManager.FindByIdAsync(userId.ToString()) ?? throw new UserNotFoundException(userId);

            var account = _repositoryManager.Account.SelectById(accountId) ?? throw new AccountNotFoundException(accountId);

            account.Name = updateAccountDto.Name ?? account.Name;
            account.ColourHex = updateAccountDto.ColourHex ?? account.ColourHex;
            account.Balance = updateAccountDto.Balance ?? account.Balance;
            account.LastModified = updateAccountDto.LastModified;

            account = _repositoryManager.Account.UpdateAccount(account);
            return _mapper.Map<AccountDto>(account);
        }
    }
}
