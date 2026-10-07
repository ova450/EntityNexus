
using System.Reflection;
using System.Linq;
using EntityNexus.Abstractions.DomainModel.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EntityNexus.Abstractions.DomainService;

/// <summary>
/// Абстрактный базовый класс для DbContext в EntityNexus.
/// Сканирует сборку доменной модели и автоматически регистрирует сущности и простые связи "зависимая -> родитель".
/// </summary>
public abstract class ADbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Получаем сборку с доменной моделью. По умолчанию — текущая исполняемая сборка,
        // можно переопределить GetDomainAssembly() в производном контексте для другой сборки.
        var domainAssembly = GetDomainAssembly();

        var entityInterfaceOpen = typeof(IEntity<>);
        var hasPrimaryOpen1 = typeof(IHasPrimaryEntity<>);
        var hasPrimaryOpen2 = typeof(IHasPrimaryEntity<,>);

        // Перебираем все типы в сборке доменной модели
        foreach (var type in domainAssembly.GetTypes())
        {
            if (type.IsAbstract || type.IsInterface)
                continue;

            // Проверяем, реализует ли тип IEntity<>
            var implementsEntity = type.GetInterfaces()
                .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == entityInterfaceOpen);

            if (!implementsEntity)
                continue;

            // Регистрируем сущность в модели EF Core
            var entityBuilder = modelBuilder.Entity(type);

            // Попробуем явно установить ключ по свойству Id (если есть)
            var idProp = type.GetProperty("Id");
            if (idProp != null)
            {
                // HasKey принимает имена свойств для неглубокого API
                try
                {
                    entityBuilder.HasKey("Id");
                }
                catch
                {
                    // игнорируем, если EF уже настроил ключ иначе
                }
            }

            // Если тип реализует IHasPrimaryEntity<...>, настраиваем связь "HasOne -> WithMany" с ключом ForeignId
            var primaryIf = type.GetInterfaces()
                .FirstOrDefault(i => i.IsGenericType && (i.GetGenericTypeDefinition() == hasPrimaryOpen1 || i.GetGenericTypeDefinition() == hasPrimaryOpen2));

            if (primaryIf != null)
            {
                Type primaryType;
                Type foreignKeyType;

                if (primaryIf.GetGenericTypeDefinition() == hasPrimaryOpen2)
                {
                    var args = primaryIf.GetGenericArguments();
                    primaryType = args[0];
                    foreignKeyType = args[1];
                }
                else
                {
                    // IHasPrimaryEntity<TPrimaryEntity> -> подразумевается int
                    primaryType = primaryIf.GetGenericArguments()[0];
                    foreignKeyType = typeof(int);
                }

                // Названия свойств по соглашению
                const string fkName = "ForeignId";
                const string navigationName = "PrimaryEntity";

                // Регистрируем отношение: у зависимой сущности есть ссылка на родителя
                try
                {
                    entityBuilder
                        .HasOne(primaryType, navigationName)
                        .WithMany(null)
                        .HasForeignKey(fkName);
                }
                catch
                {
                    // игнорируем ошибки настройки, оставляем EF по умолчанию
                }
            }
        }
    }

    /// <summary>
    /// Возвращает сборку, в которой ищутся доменные сущности для регистрации в модели.
    /// По умолчанию — Assembly.GetExecutingAssembly().
    /// Переопределите в производном классе, если сущности находятся в другой сборке.
    /// </summary>
    protected virtual Assembly GetDomainAssembly() => Assembly.GetExecutingAssembly();
}
