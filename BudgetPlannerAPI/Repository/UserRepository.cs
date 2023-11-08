using Common.Models;

using Repository.Contracts;

namespace Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(RepositoryContext context) : base(context) { }
        public User CreateUser(User user)
        {
            return Create(user);
        }

        public void DeleteUser(User user)
        {
            Delete(user);
        }

        public List<User> SelectAll(bool trackChanges = false)
        {
            return FindAll(trackChanges).ToList();
        }

        public User? SelectByEmail(string email, bool trackChanges = false)
        {
            return FindByCondition(user => user.Email == email, trackChanges).FirstOrDefault();
        }

        public User? SelectById(Guid userId, bool trackChanges = false)
        {
            return FindByCondition(user => user.Id == userId, trackChanges).FirstOrDefault();
        }

        public User UpdateUser(User user)
        {
            return Update(user);
        }
    }
}
