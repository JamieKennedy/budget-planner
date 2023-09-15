using Common.DataTransferObjects.Account;

namespace Services.Contracts
{
    public interface IAccountService
    {
        Task<AccountDto> CreateAccount(Guid userId, CreateAccountDto createAccountDto);
        Task<AccountDto> UpdateAccount(Guid userId, Guid accountId, UpdateAccountDto updateAccountDto);
        Task DeleteAccount(Guid userId, Guid accountId);
        Task<AccountDto> SelectById(Guid userId, Guid accountId, bool trackChanges = false);
        Task<IEnumerable<AccountDto>> SelectByUserId(Guid userId, bool trackChanges = false);

    }
}
