
using EntityNexus.Abstractions.DomainModel.Interfaces;
using EntityNexus.Abstractions.DomainService.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EntityNexus.Abstractions.DomainService;

/// <summary>
/// Общий репозиторий для доступа к сущностям через EF Core.
/// Реализует базовые операции чтения/записи поверх <see cref="DbSet{TEntity}"/>.
/// </summary>
/// <typeparam name="TEntity">Тип сущности.</typeparam>
/// <typeparam name="TKey">Тип ключа сущности.</typeparam>
/// <remarks>Опущены дополнительные оптимизации; реализации рассчитаны на простые сценарии.</remarks>
public class ARepository<TEntity, TKey>(ADbContext context) : IRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
    where TKey : IEquatable<TKey>
{
    protected readonly DbSet<TEntity> _dbSet =
        (context ?? throw new ArgumentNullException(nameof(context))).Set<TEntity>();

    /// <summary>Возвращает базовый IQueryable для запросов.</summary>
    public IQueryable<TEntity> GetAll() => _dbSet;

    /// <summary>Добавляет сущность в контекст.</summary>
    public EntityEntry<TEntity> Add(TEntity entity) => _dbSet.Add(entity);

    /// <summary>Обновляет сущность в контексте.</summary>
    public EntityEntry<TEntity> Update(TEntity entity) => _dbSet.Update(entity);

    /// <summary>Удаляет сущность из контекста.</summary>
    public EntityEntry<TEntity> Remove(TEntity entity) => _dbSet.Remove(entity);

    /// <summary>Получает сущность по ключу (синхронно).</summary>
    public TEntity? GetItem(TKey id) => _dbSet.Find(id);

    /// <summary>Получает все сущности как список (синхронно).</summary>
    public List<TEntity> GetList() => [.. _dbSet];

    /// <summary>Асинхронно находит сущность по ключу.</summary>
    public async Task<TEntity?> GetItemAsync(TKey id, CancellationToken cancellationToken = default)
        => await _dbSet.FindAsync(new object?[] { id }, cancellationToken);

    /// <summary>Асинхронно получает список всех сущностей.</summary>
    public Task<List<TEntity>> GetListAsync(CancellationToken cancellationToken = default)
        => _dbSet.ToListAsync(cancellationToken);

    /// <summary>Асинхронно удаляет сущность по ключу, возвращает true если удалена.</summary>
    public async Task<bool> RemoveAsync(TKey id, CancellationToken cancellationToken = default)
    {
        var entity = await GetItemAsync(id, cancellationToken);
        if (entity is null) return false;
        _dbSet.Remove(entity);
        return true;
    }

    /// <summary>Длинный подсчёт элементов.</summary>
    public long LongCount() => GetAll().LongCount();

    /// <summary>Количество элементов.</summary>
    public int Count() => GetAll().Count();

    /// <summary>Асинхронный длинный подсчёт элементов.</summary>
    public Task<long> LongCountAsync(CancellationToken cancellationToken = default)
        => _dbSet.LongCountAsync(cancellationToken);

    /// <summary>Асинхронный подсчёт элементов.</summary>
    public Task<int> CountAsync(CancellationToken cancellationToken = default)
        => _dbSet.CountAsync(cancellationToken);
}

/// <summary>
/// Упрощённая версия репозитория для сущностей с ключом типа <see cref="int"/>.
/// </summary>
/// <typeparam name="TEntity">Тип сущности.</typeparam>
public class ARepository<TEntity>(ADbContext context)
    : ARepository<TEntity, int>(context)
    , IRepository<TEntity, int>
    where TEntity : class, IEntity<int>;
