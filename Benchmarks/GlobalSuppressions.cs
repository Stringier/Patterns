using System.Diagnostics.CodeAnalysis;

// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

[assembly: SuppressMessage("Security", "MA0009:Add timeout parameter", Justification = "It's benchmarking code. Timeing out causes misleading results.")]
[assembly: SuppressMessage("Minor Code Smell", "S101:Types should be named in PascalCase", Justification = "Oh ffs Sonar, IPv4 is a very well known tech thing.")]
