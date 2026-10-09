
using EN.Tests;
using EntityNexus.Abstractions.DomainModel.Abstracts;
using EntityNexus.Abstractions.DomainService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

// Консольный стенд проверки: показывает, какую схему БД строит ADbContext из тестовых сущностей.
// Запуск пересоздаёт БД целиком; результат смотреть на ER-диаграмме (SSMS → Database Diagrams).
// Тестовые сущности:
//   - DatabaseCreating/Bar.cs — цепочка Bar <— Bar1 <— … <— Bar5 (эталон: по одному родителю);
//   - DatabaseCreating/Foo.cs — цепочка Foo <— Foo1 <— … <— Foo5; у Foo2 попытка второй связи на Bar2
//     (в схеме эта связь НЕ создаётся — см. комментарий к Foo2);
//   - DemoPrimary / DemoDependent (внизу этого файла) — простейшая пара «родитель — зависимая сущность».
// Все сущности находятся автоматически сканированием сборки (DbSet-свойства в DemoDbContext не нужны).

// Создадим простой хост для демонстрации работы ADbContext
var builder = Host.CreateDefaultBuilder(args: Array.Empty<string>())
    .ConfigureAppConfiguration((ctx, cfg) => cfg.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true))
    //// Конфигурация in-memory БД для тестирования
    //.ConfigureServices((ctx, srv) => srv.AddDbContext<DemoDbContext>(options => options.UseInMemoryDatabase("EN_Test_Db")))
    // Конфигурация SQL-сервер БД для тестирования
    // (строка подключения "DefaultConnection" берётся из appsettings.json)
    .ConfigureServices((ctx, srv) => srv.AddDbContext<DemoDbContext>(options =>
    options.UseSqlServer(ctx.Configuration.GetConnectionString("DefaultConnection"))))
    ;

using var host = builder.Build();

// Инициализация БД и пример создания сущности
using var scope = host.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<DemoDbContext>();

var model = db.Model; foreach (var et in model.GetEntityTypes()) { Console.WriteLine(et.Name); foreach (var nav in et.GetNavigations()) Console.WriteLine($" Nav: {nav.Name} -> {nav.TargetEntityType.Name}"); foreach (var fk in et.GetForeignKeys()) Console.WriteLine($" FK: {string.Join(',', fk.Properties.Select(p => p.Name))} -> {fk.PrincipalEntityType.Name}"); }

// В тестовой среде предпочитаем явно управлять созданием/очисткой БД.
// ВНИМАНИЕ: EnsureDeleted удаляет БД из строки подключения целиком, вместе с данными.
// Не указывайте в appsettings.json рабочую базу.
db.Database.EnsureDeleted();
db.Database.EnsureCreated();

//// Пример добавления данных, если требуется
//if (!db.Set<DemoPrimary>().Any())
//{
//    db.Set<DemoPrimary>().Add(new DemoPrimary { Name = "Primary 1" });
//    db.Set<DemoPrimary>().Add(new DemoPrimary { Name = "Primary 2" });
//    db.Set<DemoDependent>().Add(new DemoDependent { Name = "Dependent 1" });
//}
// Блок добавления данных выше сейчас закомментирован, поэтому SaveChanges вызывается безусловно
// и фактически ничего не сохраняет.
db.SaveChanges();

// Локальные тестовые типы и контекст

/// <summary>Контекст стенда. Дополнительных настроек не требует: сущности подхватываются автоматически.</summary>
public class DemoDbContext(DbContextOptions<DemoDbContext> options) : ADbContext(options);

/// <summary>Демонстрационный родитель (корень связи). Ключ — <see cref="int"/>.</summary>
public class DemoPrimary : AEntity, IName
{
    /// <summary>Имя родительской записи.</summary>
    public string Name { get; set; } = string.Empty;
}

/// <summary>
/// Демонстрационная зависимая сущность: внешний ключ PrimaryEntityId ссылается на <see cref="DemoPrimary"/> (связь 1:N).
/// </summary>
public class DemoDependent : AEntityDependent<DemoPrimary>, IName
{
    /// <summary>Имя зависимой записи.</summary>
    public string? Name { get; set; }

    /// <summary>Произвольная заметка (дополнительное поле для проверки столбцов таблицы).</summary>
    public string? Note { get; set; }
}
