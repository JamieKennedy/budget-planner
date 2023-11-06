using AutoMapper;

using Common.DataTransferObjects.Account;
using Common.Models;
using Common.Results.Error.Account;
using Common.Results.Error.User;

using FluentResults;

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

        public async Task<Result<AccountDto>> CreateAccount(Guid userId, CreateAccountDto createAccountDto)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user is null) return new UserNotFoundError(userId);

            var account = _mapper.Map<Account>(createAccountDto);
            account.UserId = user.Id;

            account = _repositoryManager.Account.CreateAccount(account);
            _repositoryManager.Save();

            var accountDto = _mapper.Map<AccountDto>(account);

            return accountDto;
        }

        public async Task<Result> DeleteAccount(Guid userId, Guid accountId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user is null) return new UserNotFoundError(userId);

            var account = _repositoryManager.Account.SelectById(accountId);

            if (account is null) return new AccountNotFoundError(accountId);

            _repositoryManager.Account.DeleteAccount(account);
            _repositoryManager.Save();

            return Result.Ok();

        }

        public async Task<Result<AccountDto>> SelectById(Guid userId, Guid accountId, bool trackChanges = false)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user is null) return new UserNotFoundError(userId);

            var account = _repositoryManager.Account.SelectById(accountId);

            if (account is null) return new AccountNotFoundError(accountId);

            var accountDto = _mapper.Map<AccountDto>(account);

            return accountDto;
        }

        public async Task<Result<List<AccountDto>>> SelectByUserId(Guid userId, bool trackChanges = false)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user is null) return new UserNotFoundError(userId);

            var accounts = _repositoryManager.Account.SelectByUserId(userId);

            var accountDtos = _mapper.Map<List<AccountDto>>(accounts) ?? new List<AccountDto>();

            return accountDtos;
        }

        public async Task<Result<AccountDto>> UpdateAccount(Guid userId, Guid accountId, UpdateAccountDto updateAccountDto)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user is null) return new UserNotFoundError(userId);

            var account = _repositoryManager.Account.SelectById(accountId);

            if (account is null) return new AccountNotFoundError(accountId);

            account.Name = updateAccountDto.Name ?? account.Name;
            account.ColourHex = updateAccountDto.ColourHex ?? account.ColourHex;
            account.Balance = updateAccountDto.Balance ?? account.Balance;
            account.LastModified = updateAccountDto.LastModified;

            account = _repositoryManager.Account.UpdateAccount(account);
            _repositoryManager.Save();

            var accountDto = _mapper.Map<AccountDto>(account);

            return accountDto;
        }
    }
}
