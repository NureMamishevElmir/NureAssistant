using System.Linq.Expressions;

namespace Repository.Repositories.Interfaces;

public interface IRepository<TEntity> where TEntity : class
{
    IQueryable<TEntity> GetAll();

    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

    void Add(TEntity entity);

    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    void Update(TEntity entity);

    void Remove(TEntity entity);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
