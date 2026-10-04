using EntityNexus.Abstractions.DomainModel.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EntityNexus.Abstractions.DomainService;

/// <summary>
/// Базовый контекст.
/// Сущности: автоматически регистрирует в модели все неабстрактные классы, реализующие IEntityBase,
/// из сборки <paramref name="entitiesAssembly"/> (DbSet в производном контексте не нужны).
/// Сохранение изменений (SaveChanges, SaveChangesAsync) унаследовано от DbContext.
/// База данных: проверяет наличие БД и создаёт её, если её нет.
/// </summary>
public abstract class ContextAbstract(DbContextOptions options, Assembly entitiesAssembly) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        foreach (var type in entitiesAssembly.GetTypes().Where(IsEntity))
            modelBuilder.Entity(type);
    }

    private static bool IsEntity(Type type)
        => type is { IsClass: true, IsAbstract: false, IsNested: false, IsGenericTypeDefinition: false }
           && typeof(IEntity).IsAssignableFrom(type);

    /// <summary>
    /// Если в сборке есть миграции, применяет их (создаёт БД при необходимости);
    /// иначе создаёт БД и схему по модели (EnsureCreated).
    /// </summary>
    public void EnsureDatabase()
    {
        var serviceProvider = ((IInfrastructure<IServiceProvider>)Database).Instance;
        var migrator = serviceProvider.GetService<IMigrator>();

        if (migrator != null)
            migrator.Migrate(); // выполняет все миграции
        else
            Database.EnsureCreated();
    }

    public async Task EnsureDatabaseAsync(CancellationToken cancellationToken = default)
    {
        var serviceProvider = ((IInfrastructure<IServiceProvider>)Database).Instance;
        var migrator = serviceProvider.GetService<IMigrator>();

        if (migrator != null)
            await migrator.MigrateAsync(null, cancellationToken);
        else
            await Database.EnsureCreatedAsync(cancellationToken);
    }
}

