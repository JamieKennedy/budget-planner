using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Repository.Contracts;

namespace Repository;

public class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    private readonly RepositoryContext _repositoryContext;

    public RepositoryBase(RepositoryContext repositoryContext)
    {
        _repositoryContext = repositoryContext;
    }

    public IQueryable<T> FindAll(bool trackChanges)
    {
        return trackChanges ? _repositoryContext.Set<T>() : _repositoryContext.Set<T>().AsNoTracking();
    }

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
    {
        return trackChanges
            ? _repositoryContext.Set<T>().Where(expression)
            : _repositoryContext.Set<T>().Where(expression).AsNoTracking();
    }

    public T Create(T entity)
    {
        _repositoryContext.Set<T>().Add(entity);

        return entity;

    }

    public T Update(T entity)
    {
        _repositoryContext.Set<T>().Update(entity);

        return entity;
    }

    public void Delete(T entity)
    {
        _repositoryContext.Set<T>().Remove(entity);
    }
}