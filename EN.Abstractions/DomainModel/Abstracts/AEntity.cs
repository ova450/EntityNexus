using EntityNexus.Abstractions.DomainModel.Interfaces;

namespace EntityNexus.Abstractions.DomainModel.Abstracts;

/// <summary>
/// Абстрактная базовая реализация сущности с типизированным ключом.
/// </summary>
/// <typeparam name="TIdKey">Тип первичного ключа сущности. Должен реализовывать <see cref="IEquatable{T}"/>.</typeparam>
public abstract class AEntity<TIdKey> : IEntity<TIdKey> where TIdKey : IEquatable<TIdKey>
{
    /// <summary>
    /// Уникальный идентификатор сущности.
    /// </summary>
    public TIdKey Id { get; set; } = default!;
}

/// <summary>
/// Упрощённая абстрактная реализация сущности с ключом типа <see cref="int"/>.
/// </summary>
public abstract class AEntity : IEntity, IEntity<int>
{
    /// <summary>
    /// Уникальный идентификатор сущности.
    /// </summary>
    public int Id { get; set; }
}