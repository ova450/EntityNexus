
using Microsoft.EntityFrameworkCore;

namespace EntityNexus.Abstractions.DomainService
{
    /// <summary>
    /// Абстрактный базовый класс для DbContext в EntityNexus.
    /// Предоставляет общие настройки, конфигурацию и helper-методы для всех контекстов проекта.
    /// </summary>
    public abstract class ADbContext : DbContext
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="ADbContext"/>.
        /// </summary>
        protected ADbContext() { }

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="ADbContext"/> с заданными опциями.
        /// </summary>
        /// <param name="options">Настройки контекста базы данных.</param>
        protected ADbContext(DbContextOptions options) : base(options) { }

        /// <summary>
        /// Настраивает модель при создании (вызывается автоматически EF Core).
        /// Здесь можно добавлять глобальные конвенции, soft-delete и т.д.
        /// </summary>
        /// <param name="modelBuilder">Builder для конфигурации модели.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Автоматическая конфигурация связей для типов, помеченных [ChildrenNameAttribute]
            modelBuilder.ApplyChildrenNameConventions();
        }

        /// <summary>
        /// Настраивает параметры подключения к базе данных (можно переопределять в наследниках).
        /// </summary>
        /// <param name="optionsBuilder">Builder для настройки опций.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}