using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Stringier.Patterns.Debugging;

namespace Stringier.Patterns.Nodes {
	/// <summary>
	/// Represents a rune literal, a pattern matching this exact rune.
	/// </summary>
	/// <remarks>
	/// This exists to box <see cref="System.Text.Rune"/> into something that we can treat as part of a pattern.
	/// </remarks>
	[SuppressMessage("Minor Bug", "S1206:\"Equals(Object)\" and \"GetHashCode()\" should be overridden in pairs", Justification = "It already is overriden, Sonar just doesn't understand this pattern.")]
	internal sealed class RuneLiteral : Literal {
		/// <summary>
		/// The <see cref="System.Text.Rune"/> being matched.
		/// </summary>
		internal readonly Rune Rune;

		/// <summary>
		/// Initialize a new <see cref="RuneLiteral"/> with the given <paramref name="rune"/>.
		/// </summary>
		/// <param name="rune"></param>
		internal RuneLiteral(Rune rune) : base(Compare.None) => Rune = rune;

		/// <summary>
		/// Initialize a new <see cref="RuneLiteral"/> with the given <paramref name="rune"/>.
		/// </summary>
		/// <param name="rune">The <see cref="System.Text.Rune"/> to parse.</param>
		/// <param name="comparisonType">The <see cref="Compare"/> to use when parsing.</param>
		internal RuneLiteral(Rune rune, Compare comparisonType) : base(comparisonType) => Rune = rune;

		/// <summary>
		/// Checks the first character in the <paramref name="source"/> against the header of this node.
		/// </summary>
		/// <remarks>
		/// This is primarily used to check whether a pattern may exist at the current position.
		/// </remarks>
		/// <param name="source">The <see cref="Source"/> to check against.</param>
		/// <returns><c>true</c> if this <see cref="Pattern"/> may be present, <c>false</c> if definately not.</returns>
		internal override Boolean CheckHeader(ref Source source) => Rune.CheckHeader(ref source);

		/// <summary>
		/// Call the Consume parser of this <see cref="Pattern"/> on the <paramref name="source"/> with the <paramref name="result"/>.
		/// </summary>
		/// <param name="source">The <see cref="Source"/> to consume.</param>
		/// <param name="result">A <see cref="Result"/> containing whether a match occured and the captured <see cref="String"/>.</param>
		/// <param name="trace">The <see cref="ITrace"/> to record steps in.</param>
		internal override void Consume(ref Source source, ref Result result, ITrace? trace) => Rune.Consume(ref source, ref result, ComparisonType, trace);

		/// <summary>
		/// Call the Neglect parser of this <see cref="Pattern"/> on the <paramref name="source"/> with the <paramref name="result"/>.
		/// </summary>
		/// <param name="source">The <see cref="Source"/> to consume.</param>
		/// <param name="result">A <see cref="Result"/> containing whether a match occured and the captured <see cref="String"/>.</param>
		/// <param name="trace">The <see cref="ITrace"/> to record steps in.</param>
		internal override void Neglect(ref Source source, ref Result result, ITrace? trace) => Rune.Neglect(ref source, ref result, ComparisonType, trace);
	}
}

