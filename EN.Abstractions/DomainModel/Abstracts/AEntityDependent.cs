
using EntityNexus.Abstractions.DomainModel.Interfaces;

namespace EntityNexus.Abstractions.DomainModel.Abstracts;

/// <summary>
/// Абстрактная базовая реализация зависимой сущности с типизированным ключом.
/// </summary>
/// <typeparam name="TIdKey">Тип первичного ключа сущности. Должен реализовывать <see cref="IEquatable{T}"/>.</typeparam>
///<typeparam name="TPrimaryEntity">Тип родительской сущности.</typeparam>
/// <typeparam name="TForeignKey">Тип внешнего ключа родительской сущности. Должен реализовывать <see cref="IEquatable{T}"/>.</typeparam>
public abstract class AEntityDependent<TIdKey,TPrimaryEntity, TForeignKey>:
    IEntity<TIdKey>, IHasPrimaryEntity<TPrimaryEntity, TForeignKey>  
    where TIdKey : IEquatable<TIdKey>
    where TPrimaryEntity : IEntity<TForeignKey> 
    where TForeignKey : IEquatable<TForeignKey>
{
    /// <summary>
    /// Уникальный идентификатор сущности.
    /// </summary>
    public TIdKey Id { get; set; } = default!;
    public TForeignKey ForeignId { get; set; } = default!;
    public TPrimaryEntity? PrimaryEntity { get; set; } = default!;
}

/// <summary>
/// Абстрактная упрощенная реализация зависимой сущности с типизированным ключом.
/// </summary>
/// <typeparam name="TIdKey">Тип первичного ключа сущности. Должен реализовывать <see cref="IEquatable{T}"/>.</typeparam>
///<typeparam name="TPrimaryEntity">Тип родительской сущности.</typeparam>
public abstract class AEntityDependent<TIdKey, TPrimaryEntity> : AEntityDependent<TIdKey, TPrimaryEntity, int>
    where TIdKey : IEquatable<TIdKey>
    where TPrimaryEntity : IEntity<int> 
    ;
