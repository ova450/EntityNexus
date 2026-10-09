
using EntityNexus.Abstractions.DomainModel.Abstracts;

namespace EN.Tests.DatabaseCreating;

// Тестовая цепочка «Bar» — эталонный пример линейной цепочки связей "один ко многим": каждая сущность зависит ровно от одной предыдущей. Bar <— Bar1 <— Bar2 <— Bar3 <— Bar4 <— Bar5   (стрелка: внешний ключ PrimaryEntityId зависимой сущности на Id родителя). 

/// <summary>Корень цепочки Bar. Не имеет родителя; наследует абстрактный класс <see cref="AEntity"/> (с ключом <see cref="int"/> по умолчанию), а также реализует интерфейс <see cref="IName"/>.</summary>
internal class Bar : AEntity, IName { public string? Name { get; set; } = "Bar"; }

/// <summary>Дочерняя сущность <see cref="Bar"/>: таблица Bar1 содержит внешний ключ PrimaryEntityId на Bar.Id.</summary>
internal class Bar1 : AEntityDependent<Bar>, IName { public string? Name { get; set; } = "Bar1"; }

/// <summary>Зависит от <see cref="Bar1"/>.</summary>
internal class Bar2 : AEntityDependent<Bar1>, IName { public string? Name { get; set; } = "Bar2"; }

/// <summary>Зависит от <see cref="Bar2"/>.</summary>
internal class Bar3 : AEntityDependent<Bar2>, IName { public string? Name { get; set; } = "Bar3"; }

/// <summary>Зависит от <see cref="Bar3"/>.</summary>
internal class Bar4 : AEntityDependent<Bar3>, IName { public string? Name { get; set; } = "Bar4"; }

/// <summary>Зависит от <see cref="Bar4"/>. Конец цепочки (глубина — 6 уровней вместе с <see cref="Bar"/>).</summary>
internal class Bar5 : AEntityDependent<Bar4>, IName { public string? Name { get; set; } = "Bar5"; }
