using Common.Models;

using Repository.Contracts;

namespace Repository
{
    public class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        public AccountRepository(RepositoryContext context) : base(context) { }
        public Account CreateAccount(Account account)
        {
            return Create(account);
        }

        public void DeleteAccount(Account account)
        {
            Delete(account);
        }

        public Account? SelectById(Guid id, bool trackChanges = false)
        {
            return FindByCondition(account => account.Id == id, trackChanges).FirstOrDefault();
        }

        public List<Account> SelectByUserId(Guid userId, bool trackChanges = false)
        {
            return FindByCondition(account => account.UserId == userId, trackChanges).ToList();
        }

        public Account UpdateAccount(Account account)
        {
            return Update(account);
        }
    }
}
