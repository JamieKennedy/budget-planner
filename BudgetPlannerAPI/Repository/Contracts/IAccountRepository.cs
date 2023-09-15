using Common.Models;

namespace Repository.Contracts
{
    public interface IAccountRepository
    {
        Account CreateAccount(Account account);
        Account UpdateAccount(Account account);
        void DeleteAccount(Account account);
        Account? SelectById(Guid id, bool trackChanges = false);
        IEnumerable<Account> SelectByUserId(Guid userId, bool trackChanges = false);
    }
}
