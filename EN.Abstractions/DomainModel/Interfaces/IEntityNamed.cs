
namespace EntityNexus.Abstractions.DomainModel.Interfaces;

/// <summary>
/// Интерфейс для сущностей, которые обязательно имеют имя.
/// </summary>
/// <typeparam name="TKey">Тип первичного ключа сущности.</typeparam>
public interface IEntityNamed<TKey> : IEntity<TKey> where TKey : IEquatable<TKey>
{
    /// <summary>
    /// Название/имя сущности.
    /// </summary>
    string Name { get; set; }
}

/// <summary>
/// Упрощённая версия <see cref="IEntityNamed{TKey}"/> с ключом типа <see cref="int"/>.
/// </summary>
public interface IEntityNamed : IEntity
{
    /// <summary>
    /// Название/имя сущности.
    /// </summary>
    string Name { get; set; }
}