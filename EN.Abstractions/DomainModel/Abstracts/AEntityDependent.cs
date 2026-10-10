
using EntityNexus.Abstractions.DomainModel.Interfaces;

namespace EntityNexus.Abstractions.DomainModel.Abstracts;

// 1. Полная реализация (Любой ключ сущности, любой ключ родителя)
public abstract class AEntityDependent<TIdKey, TPrimaryEntity, TPrimaryEntityKey> : AEntity<TIdKey>,
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
public abstract class AEntityDependent<TIdKey, TPrimaryEntity> : AEntityDependent<TIdKey, TPrimaryEntity, int>
    where TIdKey : IEquatable<TIdKey>
    where TPrimaryEntity : IEntity<int>
    ;

// 3. Самая простая реализация (Все ключи - int, реализует упрощенный интерфейс связи)
public abstract class AEntityDependent<TPrimaryEntity> : AEntityDependent<int, TPrimaryEntity>
    where TPrimaryEntity : IEntity<int>
    ;