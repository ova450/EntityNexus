using EntityNexus.Abstractions.DomainModel.Abstracts;
using EntityNexus.Abstractions.DomainModel.Interfaces;
// using System.Collections.Generic; // больше не требуется

namespace EN.Tests.DatabaseCreating;

// Тестовая цепочка «Foo» — такая же, как «Bar», но в середине (Foo2) сделана попытка добавить вторую
// родительскую связь — на Bar2. Фактическая ER-диаграмма: цепочка Foo <— Foo1 <— Foo2 <— Foo3 <— Foo4 <— Foo5
// построена полностью, а связи между Foo2 и Bar2 НЕТ (см. комментарий к Foo2).

/// <summary>Корень цепочки Foo. Не имеет родителя; наследует <see cref="AEntity"/> (ключ <see cref="int"/>).</summary>
internal class Foo : AEntity, IName { public string? Name { get; set; } = "Foo"; }

/// <summary>Зависит от <see cref="Foo"/>.</summary>
internal class Foo1 : AEntityDependent<Foo>, IName { public string? Name { get; set; } = "Foo1"; }

// Foo2 объявлен ниже, после Foo5, — вместе с попыткой второй родительской связи.
//internal class Foo2 : AEntityDependent<Foo1>, IName { public string? Name { get; set; } = "Foo2"; }

/// <summary>Зависит от <see cref="Foo2"/>.</summary>
internal class Foo3 : AEntityDependent<Foo2>, IName { public string? Name { get; set; } = "Foo3"; }

/// <summary>Зависит от <see cref="Foo3"/>.</summary>
internal class Foo4 : AEntityDependent<Foo3>, IName { public string? Name { get; set; } = "Foo4"; }

/// <summary>Зависит от <see cref="Foo4"/>. Конец цепочки.</summary>
internal class Foo5 : AEntityDependent<Foo4>, IName { public string? Name { get; set; } = "Foo5"; }

// TODO: либо реализовать вторую связь по рекомендации из описания ниже, либо оставить Foo2 как негативный тест
// и зафиксировать это в документации (в README сейчас сказано лишь «добавить вручную»).
/// <summary>
/// Звено цепочки Foo: зависит от <see cref="Foo1"/> и — по замыслу теста — ещё и от <c>Bar2</c>
/// (эксперимент со второй родительской связью).
/// </summary>
/// <remarks>
/// <para>
/// ИТОГ ЭКСПЕРИМЕНТА (по реальной ER-диаграмме EN.Tests): работает только основная связь Foo2 → Foo1.
/// Связь Foo2 → Bar2 в БД НЕ создаётся: у таблицы Foo2 один внешний ключ <c>PrimaryEntityId</c>, и он ведёт на Foo1.
/// </para>
/// <para>Почему так происходит:</para>
/// <list type="number">
/// <item>
/// <description>
/// Навигационное свойство на <c>Bar2</c> реализовано явно (explicit interface implementation), то есть оно не
/// public. EF Core такие свойства по конвенциям не обнаруживает и в модель не включает.
/// </description>
/// </item>
/// <item>
/// <description>
/// Член <c>PrimaryEntityId</c> интерфейса <c>IPrimaryEntity&lt;int, Bar2&gt;</c> удовлетворяется тем же
/// публичным свойством, которое Foo2 унаследовал от <see cref="AEntityDependent{TPrimaryEntity}"/>. Отдельного
/// внешнего ключа для Bar2 нет, а один и тот же столбец не может одновременно ссылаться на Foo1 и на Bar2.
/// </description>
/// </item>
/// </list>
/// <para>
/// Для рабочей второй связи нужны отдельное публичное внешнее-ключевое свойство (например, <c>Bar2Id</c>) и
/// публичная навигация (<c>Bar2</c>); при необходимости — явная настройка связи в <c>OnModelCreating</c>.
/// Этот случай относится к ограничениям EN Free (см. README: «Навигационные свойства сущностей»).
/// </para>
/// </remarks>
internal class Foo2  : AEntityDependent<Foo1>, IName, IEntity
{
    public string? Name { get; set; } = "Foo2";

    /// <summary>
    /// Навигационное свойство на единственную дочернюю сущность <see cref="Bar2"/>.
    /// Добавлено публичное свойство внешнего ключа <c>Bar2Id</c>, чтобы EF Core явным образом
    /// создал отдельный FK для этой связи.
    /// </summary>
    public int? Bar2Id { get; set; }
    public Bar2? Bar2 { get; set; }
}
