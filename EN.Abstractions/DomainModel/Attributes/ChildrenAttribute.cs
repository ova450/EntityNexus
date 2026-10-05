
namespace EntityNexus.Abstractions.DomainModel.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
public sealed class ChildrenAttribute(string childrenPropertyName) : Attribute
{
    /// <summary>
    /// Имя свойства-коллекции в родительской сущности (например "SubCategories").
    /// </summary>
    public string ChildrenPropertyName 
    { get => childrenPropertyName ?? throw new ArgumentNullException(nameof(childrenPropertyName)); }

    /// <summary>
    /// (Опционально) Тип дочерней сущности. Если не указан, будет выведен из свойства.
    /// </summary>
    public Type? ChildType { get; init; }

    /// <summary>
    /// Имя навигации с дочерней стороны на родителя. Если null — связь без навигации родителя.
    /// По умолчанию "Parent".
    /// </summary>
    public string? ParentNavigation { get; init; } = "Parent";

    /// <summary>
    /// Имя FK в таблице дочерней сущности. По умолчанию "ParentId".
    /// </summary>
    public string? ForeignKey { get; init; } = "ParentId";
}