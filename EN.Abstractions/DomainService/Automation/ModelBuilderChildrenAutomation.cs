using EntityNexus.Abstractions.DomainModel.Attributes;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EntityNexus.Abstractions.DomainService.Automation;

public static class ModelBuilderChildrenAutomation
{
    /// <summary>
    /// Автоматически настраивает HasMany/WithOne для типов, помеченных [ChildrenAttribute].
    /// </summary>
    public static void ApplyChildrenConventions(this ModelBuilder modelBuilder)
    {
        var entityTypes = modelBuilder.Model.GetEntityTypes().ToList();
        foreach (var et in entityTypes)
        {
            var clrType = et.ClrType;
            if (clrType == null) continue;

            var attr = clrType.GetCustomAttribute<ChildrenAttribute>(inherit: false);
            if (attr == null) continue;

            var childType = attr.ChildType ?? InferChildTypeFromProperty(clrType, attr.ChildrenPropertyName);
            if (childType == null) continue;

            var parentBuilder = modelBuilder.Entity(clrType);

            // Настроить навигацию: HasMany(...).WithOne(...)
            parentBuilder.HasMany(childType, attr.ChildrenPropertyName)
                         .WithOne(string.IsNullOrWhiteSpace(attr.ParentNavigation) ? null : attr.ParentNavigation);

            // При заданном имени FK — явно настроить связь со стороны dependent (child)
            if (!string.IsNullOrWhiteSpace(attr.ForeignKey))
            {
                modelBuilder.Entity(childType)
                            .HasOne(clrType, string.IsNullOrWhiteSpace(attr.ParentNavigation) ? null : attr.ParentNavigation)
                            .WithMany(attr.ChildrenPropertyName)
                            .HasForeignKey(attr.ForeignKey);
            }
        }
    }

    private static Type? InferChildTypeFromProperty(Type parentType, string propName)
    {
        var prop = parentType.GetProperty(propName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        if (prop == null) return null;

        var propType = prop.PropertyType;

        if (propType.IsGenericType)
        {
            var genArgs = propType.GetGenericArguments();
            if (genArgs.Length > 0) return genArgs[0];
        }

        if (propType.IsArray) return propType.GetElementType();

        return null;
    }
}
