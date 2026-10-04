using EntityNexus.DomainModel.Interfaces.Core;

namespace EntityNexus.DomainModel.AbstractClasses.Core
{
    /// <summary>
    /// Абстрактная базовая реализация сущности с типизированным ключом.
    /// </summary>
    /// <typeparam name="TKey">Тип первичного ключа сущности. Должен реализовывать <see cref="IEquatable{T}"/>.</typeparam>
    public abstract class AEntity<TKey> : IEntity<TKey> where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// Уникальный идентификатор сущности.
        /// </summary>
        public TKey Id { get; set; } = default!;
    }

    /// <summary>
    /// Упрощённая абстрактная реализация сущности с ключом типа <see cref="int"/>.
    /// </summary>
    public abstract class AEntity : IEntity
    {
        /// <summary>
        /// Уникальный идентификатор сущности.
        /// </summary>
        public int Id { get; set; }
    }
}