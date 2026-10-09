
//namespace EntityNexus.Abstractions.DomainModel.Interfaces;

///// <summary>
///// Интерфейс для зависимых сущностей).
///// </summary>
///// <typeparam name="TPrimaryEntity">Тип родительской сущности (PrimaryEntity).</typeparam>
///// <typeparam name="TPrimaryEntityKey">Тип первичного ключа родительской сущности (PrimaryEntity).</typeparam>
//public interface IPrimaryEntity<TPrimaryEntity, TPrimaryEntityKey>
//    where TPrimaryEntity : IEntity<TPrimaryEntityKey>
//    where TPrimaryEntityKey : IEquatable<TPrimaryEntityKey>
//{
//    /// <summary>
//    /// Идентификатор родительской сущности (PrimaryEntity).
//    /// </summary>
//    TPrimaryEntityKey PrimaryEntityId { get; set; }

//    /// <summary>
//    /// Навигационное свойство на родительскую сущность (PrimaryEntity).
//    /// </summary>
//    TPrimaryEntity? PrimaryEntity { get; set; }
//}

///// <summary>
///// Упрощённая версия интерфейса для зависимых сущностей <see cref="IPrimaryEntity{TParent, TKey}"/> с ключом типа <see cref="int"/>.
///// </summary>
///// <typeparam name="TPrimaryEntity">Тип родительской сущности (PrimaryEntity).</typeparam>
//public interface IPrimaryEntity<TPrimaryEntity> : IPrimaryEntity<TPrimaryEntity, int>
//    where TPrimaryEntity : IEntity;

namespace EntityNexus.Abstractions.DomainModel.Interfaces;

// Базовый контракт связи
public interface IPrimaryEntity<TPrimaryEntityKey, TPrimaryEntity>
    where TPrimaryEntity : IEntity<TPrimaryEntityKey>
    where TPrimaryEntityKey : IEquatable<TPrimaryEntityKey>
{
    TPrimaryEntityKey PrimaryEntityId { get; set; }
    TPrimaryEntity? PrimaryEntity { get; set; }
}

// Упрощенный контракт связи: теперь он СТРОГО требует IEntity (с ключом int)
public interface IPrimaryEntity<TPrimaryEntity> : IPrimaryEntity<int, TPrimaryEntity>
    where TPrimaryEntity : IEntity<int>; 
