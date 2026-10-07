
namespace EntityNexus.Abstractions.DomainModel.Interfaces;

/// <summary>
/// Интерфейс для зависимых сущностей).
/// </summary>
/// <typeparam name="TPrimaryEntity">Тип родительской сущности (PrimaryEntity).</typeparam>
/// <typeparam name="TForeignKey">Тип первичного ключа родительской сущности (PrimaryEntity).</typeparam>
public interface IHasPrimaryEntity<TPrimaryEntity, TForeignKey>
    where TPrimaryEntity : IEntity<TForeignKey>
    where TForeignKey : IEquatable<TForeignKey>
{
    /// <summary>
    /// Идентификатор родительской сущности (PrimaryEntity).
    /// </summary>
    TForeignKey ForeignId { get; set; }

    /// <summary>
    /// Навигационное свойство на родительскую сущность (PrimaryEntity).
    /// </summary>
    TPrimaryEntity? PrimaryEntity { get; set; }
}

/// <summary>
/// Упрощённая версия интерфейса для зависимых сущностей <see cref="IHasPrimaryEntity{TParent, TKey}"/> с ключом типа <see cref="int"/>.
/// </summary>
/// <typeparam name="TPrimaryEntity">Тип родительской сущности (PrimaryEntity).</typeparam>
public interface IHasPrimaryEntity<TPrimaryEntity> : IHasPrimaryEntity<TPrimaryEntity, int>
    where TPrimaryEntity : IEntity<int>;
