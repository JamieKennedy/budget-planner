using Common.Models;

using Repository.Contracts;

namespace Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public User CreateUser(User user)
        {
            return Create(user);
        }

        public IEnumerable<User> SelectAll(bool trackChanges = false)
        {
            return FindAll(trackChanges);
        }

        public User? SelectById(long userId, bool trackChanges = false)
        {
            return FindByCondition((user) => user.UserId == userId, trackChanges).FirstOrDefault();
        }
    }
}
