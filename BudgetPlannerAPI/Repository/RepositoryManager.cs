using Repository.Contracts;

namespace Repository;

public class RepositoryManager : IRepositoryManager
{
    private readonly RepositoryContext _repositoryContext;

    // Repositories
    private readonly Lazy<IUserRepository> _userRepository;
    private readonly Lazy<ISavingRepository> _savingRepository;
    private readonly Lazy<ISavingBalanceRepository> _savingBalanceRepository;

    public RepositoryManager(RepositoryContext repositoryContext)
    {
        _repositoryContext = repositoryContext;
        _userRepository = new Lazy<IUserRepository>(() => new UserRepository(repositoryContext));
        _savingRepository = new Lazy<ISavingRepository>(() => new SavingRepository(repositoryContext));
        _savingBalanceRepository = new Lazy<ISavingBalanceRepository>(() => new SavingBalanceRepository(repositoryContext));
    }

    public IUserRepository User => _userRepository.Value;

    public ISavingRepository Saving => _savingRepository.Value;

    public ISavingBalanceRepository SavingBalance => _savingBalanceRepository.Value;

    public void Save()
    {
        _repositoryContext.SaveChanges();
    }
}