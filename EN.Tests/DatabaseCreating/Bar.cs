
using EntityNexus.Abstractions.DomainModel.Abstracts;

namespace EN.Tests.DatabaseCreating;

internal class Bar : AEntity, IName { public string? Name { get; set; } = "Bar"; }
internal class Bar1 : AEntityDependent<Bar>, IName { public string? Name { get; set; } = "Bar1"; }
internal class Bar2 : AEntityDependent<Bar1>, IName { public string? Name { get; set; } = "Bar2"; }
internal class Bar3 : AEntityDependent<Bar2>, IName { public string? Name { get; set; } = "Bar3"; }
internal class Bar4 : AEntityDependent<Bar3>, IName { public string? Name { get; set; } = "Bar4"; }
internal class Bar5 : AEntityDependent<Bar4>, IName { public string? Name { get; set; } = "Bar5"; }
