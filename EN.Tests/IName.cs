
namespace EN.Tests
{
    /// <summary>
    /// Вспомогательный контракт тестовых сущностей: наличие текстового свойства <see cref="Name"/>.
    /// Используется только в EN.Tests, не является частью EntityNexus Free, добавлен в EntityNexus Plus.
    /// </summary>
    internal interface IName
    {
        /// <summary>
        /// Имя сущности. В тестовых сущностях в качестве имени используется имя класса ("Bar", "Foo1" и т. д.).
        /// </summary>
        string? Name { get; set; } 
    }
}
