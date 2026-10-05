
namespace EntityNexus.Abstractions.DomainModel.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
public sealed class ParentAttribute(string parentPropertyName) : Attribute
{
    /// <summary>
    /// Имя свойства-коллекции в родительской сущности (например "SubCategories").
    /// </summary>
    public string ParentPropertyName
    { get => parentPropertyName ?? throw new ArgumentNullException(nameof(parentPropertyName)); }

    /// <summary>
    /// (Опционально) Тип родительской сущности. Если не указан, будет выведен из свойства.
    /// </summary>
    public Type? ParentType { get; init; }

    /// <summary>
    /// Имя навигации с родительской стороны на дочернюю. Если null — связь без навигации дочерней.
    /// По умолчанию "Children  ".
    /// </summary>
    public string? ChildrenNavigation { get; init; } = "Children";
}