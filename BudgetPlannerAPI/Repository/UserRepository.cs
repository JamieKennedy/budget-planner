using Common.Models.User;

using Repository.Contracts;

namespace Repository
{
    public class UserRepository : RepositoryBase<UserModel>, IUserRepository
    {
        public UserRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public UserModel CreateUser(UserModel user)
        {
            return Create(user);
        }

        public UserModel? SelectById(long userId, bool trackChanges = false)
        {
            return FindByCondition((user) => user.UserId == userId, trackChanges).FirstOrDefault();
        }
    }
}
