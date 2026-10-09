
using EN.Tests;
using EntityNexus.Abstractions.DomainModel.Abstracts;
using EntityNexus.Abstractions.DomainService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

// Создадим простой хост для демонстрации работы ADbContext
var builder = Host.CreateDefaultBuilder(args: Array.Empty<string>())
    .ConfigureAppConfiguration((ctx, cfg) => cfg.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true))
    //// Конфигурация in-memory БД для тестирования
    //.ConfigureServices((ctx, srv) => srv.AddDbContext<DemoDbContext>(options => options.UseInMemoryDatabase("EN_Test_Db")))
    // Конфигурация SQL-сервер БД для тестирования
    .ConfigureServices((ctx, srv) => srv.AddDbContext<DemoDbContext>(options =>
    options.UseSqlServer(ctx.Configuration.GetConnectionString("DefaultConnection"))))
    ;

using var host = builder.Build();

// Инициализация БД и пример создания сущности
using var scope = host.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<DemoDbContext>();

// В тестовой среде предпочитаем явно управлять созданием/очисткой БД
db.Database.EnsureDeleted();
db.Database.EnsureCreated();

//// Пример добавления данных, если требуется
//if (!db.Set<DemoPrimary>().Any())
//{
//    db.Set<DemoPrimary>().Add(new DemoPrimary { Name = "Primary 1" });
//    db.Set<DemoPrimary>().Add(new DemoPrimary { Name = "Primary 2" });
//    db.Set<DemoDependent>().Add(new DemoDependent { Name = "Dependent 1" });
    db.SaveChanges();
//}

// Локальные тестовые типы и контекст
public class DemoDbContext(DbContextOptions<DemoDbContext> options) : ADbContext(options);

public class DemoPrimary : AEntity, IName
{
    public string Name { get; set; } = string.Empty;
}

public class DemoDependent : AEntityDependent<DemoPrimary>, IName
{
    public string? Name { get; set; }
    public string? Note { get; set; }
}
