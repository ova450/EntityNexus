
//using EntityNexus.Abstractions.DomainModel.Interfaces;

//namespace EntityNexus.Abstractions.DomainModel.Abstracts;

///// <summary>
///// Абстрактная базовая реализация зависимой сущности с типизированным ключом.
///// </summary>
///// <typeparam name="TIdKey">Тип первичного ключа сущности. Должен реализовывать <see cref="IEquatable{T}"/>.</typeparam>
/////<typeparam name="TPrimaryEntity">Тип родительской сущности.</typeparam>
///// <typeparam name="TPrimaryEntityKey">Тип внешнего ключа родительской сущности. Должен реализовывать <see cref="IEquatable{T}"/>.</typeparam>
//public abstract class AEntityDependent<TPrimaryEntity, TIdKey,TPrimaryEntityKey>:
//    IEntity<TIdKey>, IPrimaryEntity<TPrimaryEntity, TPrimaryEntityKey>  
//    where TIdKey : IEquatable<TIdKey>
//    where TPrimaryEntity : IEntity<TPrimaryEntityKey> 
//    where TPrimaryEntityKey : IEquatable<TPrimaryEntityKey>
//{
//    /// <summary>
//    /// Уникальный идентификатор сущности.
//    /// </summary>
//    public TIdKey Id { get; set; } = default!;
//    public TPrimaryEntityKey PrimaryEntityId { get; set; } = default!;
//    public TPrimaryEntity? PrimaryEntity { get; set; } = default!;
//}

///// <summary>
///// Абстрактная упрощенная реализация зависимой сущности с типизированным ключом.
///// </summary>
///// <typeparam name="TIdKey">Тип первичного ключа сущности. Должен реализовывать <see cref="IEquatable{T}"/>.</typeparam>
/////<typeparam name="TPrimaryEntity">Тип родительской сущности.</typeparam>
//public abstract class AEntityDependent<TPrimaryEntity, TIdKey> : AEntityDependent<TPrimaryEntity, TIdKey,  int>
//    where TIdKey : IEquatable<TIdKey>
//    where TPrimaryEntity : IEntity<int> 
//    ;

///// <summary>
///// Абстрактная упрощенная реализация зависимой сущности с типизированным ключом.
///// </summary>
///// <typeparam name="TIdKey">Тип первичного ключа сущности. Должен реализовывать <see cref="IEquatable{T}"/>.</typeparam>
/////<typeparam name="TPrimaryEntity">Тип родительской сущности.</typeparam>
//public abstract class AEntityDependent<TPrimaryEntity> : AEntityDependent<TPrimaryEntity, int>
//    where TPrimaryEntity : IEntity
//    ;

using EntityNexus.Abstractions.DomainModel.Interfaces;

namespace EntityNexus.Abstractions.DomainModel.Abstracts;

// 1. Полная реализация (Любой ключ сущности, любой ключ родителя)
public abstract class AEntityDependent<TIdKey, TPrimaryEntity, TPrimaryEntityKey> :
    IEntity<TIdKey>,
    IPrimaryEntity<TPrimaryEntityKey, TPrimaryEntity>
    where TIdKey : IEquatable<TIdKey>
    where TPrimaryEntityKey : IEquatable<TPrimaryEntityKey>
    where TPrimaryEntity : IEntity<TPrimaryEntityKey>
{
    public TIdKey Id { get; set; } = default!;
    public TPrimaryEntityKey PrimaryEntityId { get; set; } = default!;
    public TPrimaryEntity? PrimaryEntity { get; set; } = default!;
}

// 2. Промежуточная реализация (Любой ключ сущности, но родитель СТРОГО на int)
public abstract class AEntityDependent<TIdKey, TPrimaryEntity> :
    AEntityDependent<TIdKey, TPrimaryEntity, int>
    where TIdKey : IEquatable<TIdKey>
    where TPrimaryEntity : IEntity<int>    
    ;

// 3. Самая простая реализация (Все ключи - int, реализует упрощенный интерфейс связи)
public abstract class AEntityDependent<TPrimaryEntity> :
    AEntityDependent<int, TPrimaryEntity>
    where TPrimaryEntity : IEntity<int>
    ;