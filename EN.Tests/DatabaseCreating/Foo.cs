using EntityNexus.Abstractions.DomainModel.Abstracts;
using EntityNexus.Abstractions.DomainModel.Interfaces;

namespace EN.Tests.DatabaseCreating;

internal class Foo : AEntity, IName { public string? Name { get; set; } = "Foo"; }
internal class Foo1 : AEntityDependent<Foo>, IName { public string? Name { get; set; } = "Foo1"; }
//internal class Foo2 : AEntityDependent<Foo1>, IName { public string? Name { get; set; } = "Foo2"; }
internal class Foo3 : AEntityDependent<Foo2>, IName { public string? Name { get; set; } = "Foo3"; }
internal class Foo4 : AEntityDependent<Foo3>, IName { public string? Name { get; set; } = "Foo4"; }
internal class Foo5 : AEntityDependent<Foo4>, IName { public string? Name { get; set; } = "Foo5"; }

/// <summary>
/// 
/// </summary>
/// <remark>Для получения второй родительской связи можно использовать интерфейс IPrimaryEntity<>,
/// при этом следует использовать полную реализацию интерфейса</remark>
internal class Foo2 : AEntityDependent<Foo1>, IName, IPrimaryEntity<Bar2>
{
    public string? Name { get; set; }
    Bar2? IPrimaryEntity<int, Bar2>.PrimaryEntity { get; set; }
    //int IPrimaryEntity<int, Bar2>.PrimaryEntityId { get; set; }
    //Bar2? IPrimaryEntity<int, Bar2>.PrimaryEntity { get; set; }
}
