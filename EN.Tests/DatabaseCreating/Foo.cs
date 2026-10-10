using EntityNexus.Abstractions.DomainModel.Abstracts;

namespace EN.Tests.DatabaseCreating;

// Тестовая цепочка «Foo» — такая же, как «Bar», но в середине (Foo2) добавлена вторая родительская связь на Bar2. В рамках EntityNexus Free создание второй и более родительских связей не поддерживается. Поэтому, чтобы EF Core создал вторую конвенционную связь (первая создается наследованием), вторую связь и последующие связи надо добавить явно.

/// <summary>
/// Родительская сущность, корень цепочки Foo. Не имеет родителя; наследует абстрактный класс <see cref="AEntity"/> (с ключом <see cref="int"/> по умолчанию).
/// </summary>
internal class Foo : AEntity;

/// <summary>
/// Дочерняя/родительская сущность <see cref="Foo1"/> содержит внешний ключ Foo1.PrimaryEntityId на Foo.Id.
/// </summary>
internal class Foo1 : AEntityDependent<Foo>;

// Foo2 объявлен ниже, после Foo5, — вместе с попыткой второй родительской связи.
//internal class Foo2 : AEntityDependent<Foo1>2";

/// <summary>
/// Дочерняя/родительская сущность <see cref="Foo3"/> содержит внешний ключ Foo3.PrimaryEntityId на Foo2.Id.
/// </summary>
internal class Foo3 : AEntityDependent<Foo2>;

/// <summary>
/// Дочерняя/родительская сущность <see cref="Foo4"/> содержит внешний ключ Foo4.PrimaryEntityId на Foo3.Id.
/// </summary>
internal class Foo4 : AEntityDependent<Foo3>;

/// <summary>
/// Дочерняя/родительская сущность <see cref="Foo5"/> содержит внешний ключ Foo5.PrimaryEntityId на Foo4.Id.
/// </summary>
internal class Foo5 : AEntityDependent<Foo4>;

/// <summary>
/// Пример класса, который наследует <see cref="AEntityDependent{TParent}"/> и пытается добавить вторую родительскую связь.
/// В рамках EntityNexus Free создание второй и более родительских связей не поддерживается. Поэтому, чтобы EF Core создал вторую конвенционную связь (первая создается наследованием), вторую связь и последующие связи надо добавить явно. 
/// </summary>
internal class Foo2 : AEntityDependent<Foo1>
{
    public string? Name { get; set; }

    /// <summary>
    /// Навигационное свойство на вторую родительскую сущность <see cref="Bar2"/>.
    /// Добавлено публичное свойство внешнего ключа <c>Foo2Id</c>, чтобы EF Core явным образом
    /// создал отдельный FK для этой связи.
    /// </summary>
    public int? Bar2Id { get; set; }
    public Bar2? Bar2 { get; set; }
}
