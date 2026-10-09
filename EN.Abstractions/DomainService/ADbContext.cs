
using EntityNexus.Abstractions.DomainModel.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EntityNexus.Abstractions.DomainService;

/// <summary>
/// Базовый контекст EntityNexus.
/// Автоматически обнаруживает и регистрирует в модели EF Core все доменные сущности,
/// реализующие интерфейс IEntity. Связи и ключи настраиваются автоматически по конвенциям.
/// </summary>
public abstract class ADbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Получаем сборку для сканирования (переопределяется в наследниках)
        var domainAssembly = GetDomainAssembly();

        // Открытый тип интерфейса сущности для проверки
        var entityInterfaceOpen = typeof(IEntity<>);

        // Ищем все подходящие классы
        var entityTypes = domainAssembly.GetTypes()
            .Where(type => type.IsClass
                           && !type.IsAbstract
                           && type.GetInterfaces().Any(i => i.IsGenericType
                                                            && i.GetGenericTypeDefinition() == entityInterfaceOpen));

        // Просто добавляем их в модель. 
        // EF Core сам настроит первичные ключи по свойству "Id" и внешние ключи по конвенциям.
        foreach (var type in entityTypes)
        {
            modelBuilder.Entity(type);
        }
    }

    /// <summary>
    /// Возвращает сборку, в которой находятся доменные сущности.
    ///
    /// По умолчанию выполняется автоматический поиск: перебираются загруженные сборки
    /// и возвращается первая сборка, в которой найден хотя бы один конкретный (non-abstract)
    /// класс, реализующий IEntity или IEntity&lt;&gt;. Это позволяет не переопределять метод
    /// в большинстве случаев — достаточно поместить доменные сущности в любую загружаемую
    /// сборку приложения.
    ///
    /// Если автоматический поиск не даст результата — возвращается сборка текущего контекста (GetType().Assembly).
    /// Переопределите метод в редких случаях, когда нужен явный контроль.
    /// </summary>
    protected virtual Assembly GetDomainAssembly()
    {
        var entityOpen = typeof(IEntity<>);
        var entityNon = typeof(IEntity);

        // Список кандидатов: текущая, выполняемая и все загруженные сборки приложения
        var candidates = new[] { GetType().Assembly, Assembly.GetEntryAssembly(), Assembly.GetExecutingAssembly() }
            .Where(a => a != null)
            .Concat(AppDomain.CurrentDomain.GetAssemblies().Where(a => !a.IsDynamic))
            .Where(a => a != null)
            .Distinct();

        foreach (var asm in candidates)
        {
            try
            {
                var types = asm.GetTypes();
                var hasEntity = types.Any(t => t.IsClass && !t.IsAbstract && t.GetInterfaces().Any(i =>
                    (i.IsGenericType && i.GetGenericTypeDefinition() == entityOpen) || i == entityNon));

                if (hasEntity)
                    return asm;
            }
            catch
            {
                // Игнорируем сборки, которые не удаётся отсканировать
            }
        }

        return GetType().Assembly;
    }
}
