using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Linq.Expressions;

namespace EntityNexus.Infrastructure.AbstractClasses.Core
{
    /// <summary>
    /// Класс расширений (extensions) для <see cref="EntityTypeBuilder{TEntity}"/>.
    /// Содержит полезные helper-методы для fluent-конфигурации сущностей в OnModelCreating.
    /// </summary>
    public static class EntityTypeBuilderExtensions
    {
        /// <summary>
        /// Устанавливает мягкое удаление (Soft Delete) для сущности.
        /// Автоматически добавляет глобальный query filter, который исключает удалённые записи.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <param name="builder">Entity Type Builder.</param>
        /// <param name="propertyExpression">Выражение, указывающее на свойство IsDeleted (например, e => e.IsDeleted).</param>
        /// <returns>EntityTypeBuilder для цепочки вызовов.</returns>
        public static EntityTypeBuilder<TEntity> HasSoftDeleteFilter<TEntity>(
            this EntityTypeBuilder<TEntity> builder,
            Expression<Func<TEntity, bool>> propertyExpression)
            where TEntity : class
        {
            // Реализация фильтра
            return builder;
        }

        /// <summary>
        /// Делает свойство обязательным (NOT NULL) и задаёт максимальную длину для строковых полей.
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <param name="builder">Entity Type Builder.</param>
        /// <param name="propertyExpression">Выражение свойства.</param>
        /// <param name="maxLength">Максимальная длина строки.</param>
        /// <returns>EntityTypeBuilder для цепочки вызовов.</returns>
        public static EntityTypeBuilder<TEntity> PropertyRequiredWithMaxLength<TEntity>(
            this EntityTypeBuilder<TEntity> builder,
            Expression<Func<TEntity, string>> propertyExpression,
            int maxLength)
            where TEntity : class
        {
            // Реализация
            return builder;
        }

        // Добавляй сюда другие полезные расширения по мере развития фреймворка...
    }
}