using EntityNexus.DomainModel.AbstractClasses.Core;
using EntityNexus.DomainModel.Interfaces.Core;
using EntityNexus.Infrastructure.AbstractClasses.Core;
using Microsoft.EntityFrameworkCore;

namespace EntityNexus.Samples.Core;

/// <summary>
/// Содержит примеры доменных сущностей и контекст базы данных
/// для демонстрации и тестирования возможностей EntityNexus Core.
/// </summary>
public class User(string name) : AEntityNamed(name);

/// <summary>
/// Пример продукта — именованной сущности с дополнительными свойствами.
/// </summary>
public class Product(string name) : AEntityNamed(name)
{
    /// <summary>
    /// Цена продукта.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Остаток на складе.
    /// </summary>
    public int Stock { get; set; }
}

/// <summary>
/// Заказ — сущность, которая имеет родителя (пользователя) и дочерние элементы (позиции заказа).
/// </summary>
public class Order : AEntity, IHasParent<User>, IHasChild<OrderItem>
{
    /// <summary>
    /// Идентификатор пользователя (родителя заказа).
    /// </summary>
    public int ParentId { get; set; }

    /// <summary>
    /// Навигационное свойство на пользователя, оформившего заказ.
    /// </summary>
    public User? Parent { get; set; }

    /// <summary>
    /// Коллекция позиций в заказе.
    /// </summary>
    public ICollection<OrderItem> Children { get; set; } = [];

    /// <summary>
    /// Общая сумма заказа.
    /// </summary>
    public decimal TotalAmount { get; set; }
}

/// <summary>
/// Позиция заказа — элемент, входящий в состав заказа.
/// </summary>
public class OrderItem(string name) : AEntityNamed(name), IHasParent<Order>
{
    /// <summary>
    /// Идентификатор родительского заказа.
    /// </summary>
    public int ParentId { get; set; }

    /// <summary>
    /// Навигационное свойство на родительский заказ.
    /// </summary>
    public Order? Parent { get; set; }

    /// <summary>
    /// Количество единиц товара в позиции.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Цена за единицу в данной позиции.
    /// </summary>
    public decimal Price { get; set; }
}

/// <summary>
/// Контекст базы данных для демонстрационных сущностей.
/// </summary>
public class ShopDbContext(DbContextOptions<ShopDbContext> options)
    : ADbContext(options)
{
    /// <summary>
    /// Пользователи.
    /// </summary>
    public DbSet<User> Users => Set<User>();

    /// <summary>
    /// Продукты.
    /// </summary>
    public DbSet<Product> Products => Set<Product>();

    /// <summary>
    /// Заказы.
    /// </summary>
    public DbSet<Order> Orders => Set<Order>();

    /// <summary>
    /// Позиции заказов.
    /// </summary>
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
}