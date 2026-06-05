using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories;

public abstract class EntityFrameworkRepository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly DbContext Context;
    protected readonly DbSet<TEntity> DbSet;

    protected EntityFrameworkRepository(DbContext context)
    {
        Context = context;
        DbSet = context.Set<TEntity>();
    }

    public virtual IQueryable<TEntity> GetAll() => DbSet.AsQueryable();

    public virtual async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await DbSet.FindAsync(new object?[] { id }, cancellationToken);

    public virtual Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        => DbSet.FirstOrDefaultAsync(predicate, cancellationToken);

    public virtual void Add(TEntity entity) => DbSet.Add(entity);

    public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        => await DbSet.AddAsync(entity, cancellationToken);

    public virtual void Update(TEntity entity) => DbSet.Update(entity);

    public virtual void Remove(TEntity entity) => DbSet.Remove(entity);

    public virtual Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => Context.SaveChangesAsync(cancellationToken);
}
