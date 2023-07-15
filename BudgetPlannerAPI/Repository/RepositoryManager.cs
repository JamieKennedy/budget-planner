﻿using Repository.Contracts;

namespace Repository;

public class RepositoryManager : IRepositoryManager
{
    private readonly RepositoryContext _repositoryContext;

    // Repositories
    private readonly Lazy<ITokenRepository> _tokenRepository;
    private readonly Lazy<ISavingsRepository> _savingsRepository;
    private readonly Lazy<ISavingsBalanceRepository> _savingsBalanceRepository;
    private readonly Lazy<IExpenseCategoryRepository> _expenseCategoryRepository;
    private readonly Lazy<IContributorRepository> _contributorRepository;

    public RepositoryManager(RepositoryContext repositoryContext)
    {
        _repositoryContext = repositoryContext;
        _tokenRepository = new Lazy<ITokenRepository>(() => new TokenRepository(repositoryContext));
        _savingsRepository = new Lazy<ISavingsRepository>(() => new SavingsRepository(repositoryContext));
        _savingsBalanceRepository = new Lazy<ISavingsBalanceRepository>(() => new SavingsBalanceRepository(repositoryContext));
        _expenseCategoryRepository = new Lazy<IExpenseCategoryRepository>(() => new ExpenseCategoryRepository(repositoryContext));
        _contributorRepository = new Lazy<IContributorRepository>(() => new ContributorRepository(repositoryContext));
    }

    public ITokenRepository Tokens => _tokenRepository.Value;
    public ISavingsRepository Savings => _savingsRepository.Value;
    public ISavingsBalanceRepository SavingsBalance => _savingsBalanceRepository.Value;
    public IExpenseCategoryRepository ExpenseCategory => _expenseCategoryRepository.Value;
    public IContributorRepository Contributor => _contributorRepository.Value;

    public void Save()
    {
        _repositoryContext.SaveChanges();
    }
}