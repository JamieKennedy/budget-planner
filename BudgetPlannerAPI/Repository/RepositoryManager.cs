using Repository.Contracts;

namespace Repository;

public class RepositoryManager : IRepositoryManager
{
    private readonly RepositoryContext _repositoryContext;

    // Repositories
    private readonly Lazy<IUserRepository> _userRepository;
    private readonly Lazy<ISavingsRepository> _savingsRepository;

    public RepositoryManager(RepositoryContext repositoryContext)
    {
        _repositoryContext = repositoryContext;
        _userRepository = new Lazy<IUserRepository>(() => new UserRepository(repositoryContext));
        _savingsRepository = new Lazy<ISavingsRepository>(() => new SavingsRepository(repositoryContext));
    }

    public IUserRepository User => _userRepository.Value;

    public ISavingsRepository Savings => _savingsRepository.Value;

    public void Save()
    {
        _repositoryContext.SaveChanges();
    }
}