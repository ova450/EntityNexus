using EntityNexus.Abstractions.DomainModel.Interfaces;
using EntityNexus.Abstractions.DomainService.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EntityNexus.Abstractions.DomainService;

//public class RepositoryAbstract<TEntity>(ContextAbstract context) : RepositoryAbstract<TEntity,int>(context);

/// <summary>
/// Опущены методы Count и CountAsync, так как они доступны через IQueryable GetAll().
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TKey"></typeparam>
/// <param name="context"></param>
public class ARepository<TEntity, TKey>(ADbContext context) : IRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
    where TKey : IEquatable<TKey>
{
    protected readonly DbSet<TEntity> _dbSet =
        (context ?? throw new ArgumentNullException(nameof(context))).Set<TEntity>();

    public IQueryable<TEntity> GetAll() => _dbSet;

    public EntityEntry<TEntity> Add(TEntity entity) => _dbSet.Add(entity);

    public EntityEntry<TEntity> Update(TEntity entity) => _dbSet.Update(entity);

    public EntityEntry<TEntity> Remove(TEntity entity) => _dbSet.Remove(entity);

    public TEntity? GetItem(TKey id) => _dbSet.Find(id);

    public List<TEntity> GetList() => [.. _dbSet];


    public async Task<TEntity?> GetItemAsync(TKey id, CancellationToken cancellationToken = default)
        => await _dbSet.FindAsync([id], cancellationToken);

    public Task<List<TEntity>> GetListAsync(CancellationToken cancellationToken = default)
        => _dbSet.ToListAsync(cancellationToken);
        
    public async Task<bool> RemoveAsync(TKey id, CancellationToken cancellationToken = default)
    {
        var entity = await GetItemAsync(id, cancellationToken);
        if (entity is null) return false;
        _dbSet.Remove(entity);
        return true;
    }

    public long LongCount() => GetAll().LongCount();
    public int Count() => GetAll().Count();

    public Task<long> LongCountAsync(CancellationToken cancellationToken = default)
        => _dbSet.LongCountAsync(cancellationToken);
    public Task<int> CountAsync(CancellationToken cancellationToken = default)
        => _dbSet.CountAsync(cancellationToken);
}

public class ARepository<TEntity>(ADbContext context) 
    : ARepository<TEntity, int>(context)
    , IRepository<TEntity, int>
    where TEntity : class, IEntity<int>;
