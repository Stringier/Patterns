// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Style", "IDE0058:Expression value is never used", Justification = "It's fine. It's just discards for testing.")]
[assembly: SuppressMessage("Performance", "HAA0101:Array allocation for params parameter", Justification = "It's just testing code, it's fine.")]
[assembly: SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "Sonar doesn't understand this, wow, huge surprise.")]
