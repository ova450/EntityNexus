
using EntityNexus.Abstractions.DomainModel.Abstracts;

namespace EN.Tests.DatabaseCreating;

// Тестовая цепочка «Bar» — эталонный пример линейной цепочки связей "один ко многим": каждая сущность зависит ровно от одной предыдущей. Bar <— Bar1 <— Bar2 <— Bar3 <— Bar4 <— Bar5   (стрелка: внешний ключ PrimaryEntityId зависимой сущности на Id родителя). 

/// <summary>
/// Родительская сущность, корень цепочки Bar. Не имеет родителя; наследует абстрактный класс <see cref="AEntity"/> (с ключом <see cref="int"/> по умолчанию).
/// </summary>
internal class Bar : AEntity;

/// <summary>
/// Дочерняя/родительская сущность <see cref="Bar1"/> содержит внешний ключ Bar1.PrimaryEntityId на Bar.Id.
/// </summary>
internal class Bar1 : AEntityDependent<Bar>;

/// <summary>
/// Дочерняя/родительская сущность <see cref="Bar2"/> содержит внешний ключ Bar2.PrimaryEntityId на Bar1.Id.
/// </summary>
internal class Bar2 : AEntityDependent<Bar1>;

/// <summary>
/// Дочерняя/родительская сущность <see cref="Bar3"/> содержит внешний ключ Bar3.PrimaryEntityId на Bar2.Id.
/// </summary>
internal class Bar3 : AEntityDependent<Bar2>;

/// <summary>
/// Дочерняя/родительская сущность <see cref="Bar4"/> содержит внешний ключ Bar4.PrimaryEntityId на Bar3.Id.
/// </summary>
internal class Bar4 : AEntityDependent<Bar3>;

/// <summary>
/// Дочерняя/родительская сущность <see cref="Bar5"/> содержит внешний ключ Bar5.PrimaryEntityId на Bar4.Id.
/// </summary>
internal class Bar5 : AEntityDependent<Bar4>;
