
namespace EntityNexus.Abstractions.DomainModel.Interfaces;

/// <summary>
/// Базовый интерфейс для всех сущностей с типизированным идентификатором.
/// </summary>
/// <typeparam name="TKey">Тип первичного ключа сущности (int, Guid, string и т.д.). Должен реализовывать IEquatable&lt;TIdKey&gt;.</typeparam>
public interface IEntity<TKey> where TKey : IEquatable<TKey>
{
    /// <summary>
    /// Уникальный идентификатор сущности.
    /// </summary>
    TKey Id { get; set; }
}

// Упрощенный маркерный интерфейс
public interface IEntity : IEntity<int>;
