
namespace EntityNexus.Abstractions.DomainModel.Interfaces;

/// <summary>
/// Интерфейс для сущностей, которые могут иметь дочерние элементы (иерархия "родитель → дети").
/// </summary>
/// <typeparam name="TChild">Тип дочерней сущности.</typeparam>
/// <typeparam name="TKey">Тип первичного ключа.</typeparam>
public interface IHasChild<TChild, TKey>
    where TKey : IEquatable<TKey>
    where TChild : IEntity<TKey>
{
    /// <summary>
    /// Коллекция дочерних сущностей.
    /// </summary>
    ICollection<TChild> Children { get; set; }
}

/// <summary>
/// Упрощённая версия <see cref="IHasChild{TChild, TKey}"/> с ключом типа <see cref="int"/>.
/// </summary>
/// <typeparam name="TChild">Тип дочерней сущности.</typeparam>
public interface IHasChild<TChild> where TChild : IEntity
{
    /// <summary>
    /// Коллекция дочерних сущностей.
    /// </summary>
    ICollection<TChild> Children { get; set; }
}