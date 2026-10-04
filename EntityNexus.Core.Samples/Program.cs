using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace EntityNexus.Samples.Core;

/// <summary>
/// Демонстрационная программа (точка входа), показывающая базовое использование EntityNexus Core:
/// создание сущностей, работа с иерархией, сохранение в EF Core.
/// </summary>
internal class Program
{
    /// <summary>
    /// Главная точка входа в консольное приложение.
    /// </summary>
    /// <param name="args">Аргументы командной строки.</param>
    /// <returns>Task для асинхронного выполнения.</returns>
    private static async Task Main(string[] args)
    {
        var services = new ServiceCollection();

        var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection")
    ?? "Server=(localdb)\\mssqllocaldb;Database=ENM_Sample_17;Trusted_Connection=True;";

        //options.UseSqlServer(connectionString);

        services.AddDbContext<ShopDbContext>(opt => opt.UseSqlServer(connectionString));

        var provider = services.BuildServiceProvider();

        using var scope = provider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ShopDbContext>();

        // Подготовка базы данных
        //await db.Database.EnsureDeletedAsync();
        await db.Database.EnsureCreatedAsync();

        // 1. Создаем пользователя
        User user = new("John");
        user.Rename("Tom");

        // 2. Создаем продукт
        var product = new Product("Laptop")
        {
            Price = 1000,
            Stock = 5
        };

        db.AddRange(user, product);

        user.Rename("Gary");

        await db.SaveChangesAsync();

        // 3. Создаем заказ
        var order = new Order
        {
            Parent = user,
            TotalAmount = product.Price,
            Children =
            [
                new OrderItem("Laptop")
                {
                    Quantity = 1,
                    Price = product.Price
                }
            ]
        };

        product.Stock -= 1;

        db.Orders.Add(order);

        product.Rename("New Name");
        product.Price = 200;

        await db.SaveChangesAsync();

        Console.WriteLine("Done");
    }
}