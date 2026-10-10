
using EN.Tests;
using EntityNexus.Abstractions.DomainModel.Abstracts;
using EntityNexus.Abstractions.DomainService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

/// <summary>Консольный стенд проверки: показывает, какую схему БД строит ADbContext из тестовых сущностей.
/// Запуск пересоздаёт БД целиком; результат смотреть на ER-диаграмме (SSMS → Database Diagrams).
/// Тестовые сущности:
///  - DatabaseCreating/Bar.cs — цепочка Bar <— Bar1 <— … <— Bar5 (эталон: по одному родителю);
///   - DatabaseCreating/Foo.cs — цепочка Foo <— Foo1 <— … <— Foo5; у Foo2 попытка второй связи на Bar2
///     (в схеме эта связь НЕ создаётся — см. комментарий к Foo2);
///   - DemoPrimary / DemoDependent (внизу этого файла) — простейшая пара «родитель — зависимая сущность».
/// Все сущности находятся автоматически сканированием сборки (DbSet-свойства в DemoDbContext не нужны).
///</summary>

// Простой хост для демонстрации работы ADbContext
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
// Блок добавления данных выше сейчас закомментирован, поэтому:
// SaveChanges вызывается безусловно и сохраняет вновь созданную пустую базу данных.
db.SaveChanges();

// Локальные тестовые типы и контекст

/// <summary>Контекст стенда. Дополнительных настроек не требует: сущности подхватываются автоматически.</summary>
public class DemoDbContext(DbContextOptions<DemoDbContext> options) : ADbContext(options);

/// <summary>Демонстрационная сущность, наследуется от абстрактного типа <see cref="AEntity"/> без параметра типа, что предполагает автоматическое формирование первичного ключа типа <see cref="int"/> и исключительно родительскую роль в связях.</summary>
public class DemoPrimary : AEntity
{
    /// <summary>Имя родительской записи.</summary>
    public string Name { get; set; } = string.Empty;
}

/// <summary>
/// Демонстрационная сущность наследуется от абстрактного типа <see cref="AEntityDependent"/>.
/// Параметры типа:
/// TIdKey - опущен, определяет первичный ключ <see cref="Id"/> как <see cref="int"/> по умолчанию; 
/// TPrimaryEntity - <see cref="DemoPrimary"/>, определяет навигационное свойство <see cref="PrimaryEntity"/> на родительскую сущность;
/// TPrimaryEntityKey> - опущен, определяет первичный ключ родительской сущности <see cref="PrimaryEntityId"/> как <see cref="int"/> по умолчанию:
/// </summary>
public class DemoDependent : AEntityDependent<DemoPrimary> 
{
    /// <summary>Имя зависимой записи.</summary>
    public string? Name { get; set; }

    /// <summary>Произвольная заметка (дополнительное поле для проверки столбцов таблицы).</summary>
    public string? Note { get; set; }
}
