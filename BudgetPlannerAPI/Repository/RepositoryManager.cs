using Repository.Contracts;

namespace Repository;

public class RepositoryManager : IRepositoryManager
{
    private readonly RepositoryContext _repositoryContext;

    // Repositories
    private readonly Lazy<ITokenRepository> _tokenRepository;
    private readonly Lazy<ISavingsRepository> _savingsRepository;
    private readonly Lazy<ISavingsBalanceRepository> _savingsBalanceRepository;
    private readonly Lazy<IGroupRepository> _groupRepository;
    private readonly Lazy<IGroupMemberRepository> _groupMemberRepository;

    public RepositoryManager(RepositoryContext repositoryContext)
    {
        _repositoryContext = repositoryContext;
        _tokenRepository = new Lazy<ITokenRepository>(() => new TokenRepository(repositoryContext));
        _savingsRepository = new Lazy<ISavingsRepository>(() => new SavingsRepository(repositoryContext));
        _savingsBalanceRepository = new Lazy<ISavingsBalanceRepository>(() => new SavingsBalanceRepository(repositoryContext));
        _groupRepository = new Lazy<IGroupRepository>(() => new GroupRepository(repositoryContext));
        _groupMemberRepository = new Lazy<IGroupMemberRepository>(() => new GroupMemberRepository(repositoryContext));
    }

    public ITokenRepository Tokens => _tokenRepository.Value;
    public ISavingsRepository Savings => _savingsRepository.Value;
    public ISavingsBalanceRepository SavingsBalance => _savingsBalanceRepository.Value;
    public IGroupRepository Group => _groupRepository.Value;
    public IGroupMemberRepository GroupMember => _groupMemberRepository.Value;

    public void Save()
    {
        _repositoryContext.SaveChanges();
    }
}