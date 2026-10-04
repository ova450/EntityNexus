using EntityNexus.Abstractions.DomainModel.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EntityNexus.Abstractions.DomainService.Interfaces;

/// <summary>
/// Методы сохранения для репозитория.
/// </summary>
public interface IRepository<TEntity> : IRepository<TEntity, int> where TEntity : class, IEntity;

public interface IRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
    where TKey : IEquatable<TKey>
{
    #region Методы БЕЗ ОБРАЩЕНИЯ к БД: строят запрос или меняют состояние в ChangeTracker

    /// <summary>Запрос ко всем записям. Выполняется у вызывающего (ToListAsync, FirstOrDefaultAsync и т. д.).</summary>
    IQueryable<TEntity> GetAll();

    EntityEntry<TEntity> Add(TEntity entity);

    /// <summary>Помечает сущность (и связанные с ней) как изменённую. Нужен для отсоединённых сущностей.</summary>
    EntityEntry<TEntity> Update(TEntity entity);

    EntityEntry<TEntity> Remove(TEntity entity);

    #endregion

    #region Методы, ОБРАЩАЮЩИЕСЯ к БД

    #region СИНХРОННЫЕ методы (блокируют поток)

    /// <summary>Возвращает сущность по Id или null, если она не найдена.</summary>
    TEntity? GetItem(TKey id);

    List<TEntity> GetList();

    int Count();

    long LongCount();

    #endregion

    #region АСИНХРОННЫЕ методы (не блокируют поток, предпочтительно использовать их)

    /// <summary>Возвращает сущность по Id или null, если она не найдена.</summary>
    Task<TEntity?> GetItemAsync(TKey id, CancellationToken cancellationToken = default);

    Task<List<TEntity>> GetListAsync(CancellationToken cancellationToken = default);

    Task<int> CountAsync(CancellationToken cancellationToken = default);
    Task<long> LongCountAsync(CancellationToken cancellationToken = default);


    /// <summary>Находит сущность по Id и помечает её на удаление. Возвращает false, если не найдена.</summary>
    Task<bool> RemoveAsync(TKey id, CancellationToken cancellationToken = default);

    #endregion
    #endregion
}
