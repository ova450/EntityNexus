
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using EntityNexus.Abstractions.DomainService;
using System.Reflection;

// Создадим простой хост для демонстрации работы ADbContext
var builder = Host.CreateDefaultBuilder(args: Array.Empty<string>())
    .ConfigureAppConfiguration((ctx, cfg) =>
    {
        cfg.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
    })
    .ConfigureServices((ctx, services) =>
    {
        // Конфигурация in-memory БД для тестирования
        services.AddDbContext<DemoDbContext>(options =>
            options.UseInMemoryDatabase("EN_Test_Db"));
    });

using var host = builder.Build();

// Инициализация БД и пример создания сущности
using (var scope = host.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DemoDbContext>();

    // Создаём и сохраняем пример сущности
    var example = new DemoPrimary { Id = 1, Name = "Primary 1" };
    db.Set<DemoPrimary>().Add(example);
    db.SaveChanges();

    Console.WriteLine("DemoPrimary saved. Count: " + db.Set<DemoPrimary>().Count());
}

Console.WriteLine("Done.");

// Локальные тестовые типы и контекст
public class DemoDbContext : ADbContext
{
    public DemoDbContext(DbContextOptions<DemoDbContext> options) : base(options) { }

    protected override Assembly GetDomainAssembly() => typeof(DemoPrimary).Assembly;
}

public class DemoPrimary : EntityNexus.Abstractions.DomainModel.Abstracts.AEntity<int>
{
    public string Name { get; set; } = string.Empty;
}

public class DemoDependent : EntityNexus.Abstractions.DomainModel.Abstracts.AEntityDependent<int, DemoPrimary>
{
    public string? Note { get; set; }
}
