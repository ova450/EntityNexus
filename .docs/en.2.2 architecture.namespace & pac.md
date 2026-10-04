# Namespace & Package Strategy

EntityNexus intentionally separates deployment topology from semantic topology.

Most projects make package hierarchy and namespace hierarchy identical.  
EntityNexus treats them as different concerns.

---

# Distribution Topology

Package and assembly naming primarily describe deployment boundaries and dependency direction.

Examples:

- `EntityNexus.Core.Interfaces`
- `EntityNexus.Core.AbstractClasses`
- `EntityNexus.Core.Samples`

This hierarchy is optimized for:
- NuGet discoverability,
- package grouping,
- dependency management,
- deployment structure.

The `Core` segment represents the architectural layer/package group rather than semantic classification.

---

# Semantic Topology

Namespaces describe semantic meaning and architectural role.

Examples:

- `EntityNexus.DomainModel.Interfaces.Core`
- `EntityNexus.DomainModel.AbstractClasses.Core`
- `EntityNexus.Infrastructure.AbstractClasses.Core`

This hierarchy is optimized for:
- code navigation,
- architectural readability,
- semantic grouping,
- ontology consistency.

Semantic role is considered more important than deployment grouping.

---

# Inverted Hierarchy

EntityNexus intentionally uses an inverted namespace hierarchy compared to package naming.

Example:

| Package                            | Namespace                                      |
| ---------------------------------- | ---------------------------------------------- |
| `EntityNexus.Core.AbstractClasses` | `EntityNexus.DomainModel.AbstractClasses.Core` |

This is intentional.

The package answers:

> Where is this deployed and versioned?

The namespace answers:

> What semantic role does this code play?

These are different questions and therefore use different hierarchies.

---

# Why Not Traditional Namespace Layout

Traditional .NET convention usually mirrors:

```text
Package == Assembly == Namespace