
namespace EntityNexus.Abstractions.DomainModel.Interfaces;

/// <summary>
/// Маркерный интерфейс для сущностей, которые имеют родителя (иерархия "дочерний → родитель").
/// </summary>
/// <typeparam name="TParent">Тип родительской сущности.</typeparam>
/// <typeparam name="TKey">Тип первичного ключа.</typeparam>
public interface IHasParent<TParent, TKey>
    where TParent : IEntity<TKey>
    where TKey : IEquatable<TKey>
{
    /// <summary>
    /// Идентификатор родительской сущности.
    /// </summary>
    TKey ParentId { get; set; }

    /// <summary>
    /// Навигационное свойство на родительскую сущность.
    /// </summary>
    TParent? Parent { get; set; }
}

/// <summary>
/// Упрощённая версия <see cref="IHasParent{TParent, TKey}"/> с ключом типа <see cref="int"/>.
/// </summary>
/// <typeparam name="TParent">Тип родительской сущности.</typeparam>
public interface IHasParent<TParent> where TParent : IEntity
{
    /// <summary>
    /// Идентификатор родительской сущности.
    /// </summary>
    int ParentId { get; set; }

    /// <summary>
    /// Навигационное свойство на родительскую сущность.
    /// </summary>
    TParent? Parent { get; set; }
}
