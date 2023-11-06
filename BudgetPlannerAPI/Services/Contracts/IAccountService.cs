using Common.DataTransferObjects.Account;

using FluentResults;

namespace Services.Contracts
{
    public interface IAccountService
    {
        Task<Result<AccountDto>> CreateAccount(Guid userId, CreateAccountDto createAccountDto);
        Task<Result<AccountDto>> UpdateAccount(Guid userId, Guid accountId, UpdateAccountDto updateAccountDto);
        Task<Result> DeleteAccount(Guid userId, Guid accountId);
        Task<Result<AccountDto>> SelectById(Guid userId, Guid accountId, bool trackChanges = false);
        Task<Result<List<AccountDto>>> SelectByUserId(Guid userId, bool trackChanges = false);

    }
}
