using System;
using System.Text;
using static Stringier.Patterns.Pattern;
using Stringier.Patterns.Debugging;
using Stringier.Patterns.Nodes;
using Defender;

namespace Stringier.Patterns {
	public static class RuneExtensions {
		/// <summary>
		/// Get the <paramref name="Rune"/> as a <see cref="Pattern"/>.
		/// </summary>
		/// <param name="Rune">The <see cref="Rune"/> literal.</param>
		/// <returns>A <see cref="Pattern"/> representing literally the <paramref name="Rune"/>.</returns>
		public static Pattern AsPattern(this Rune @Rune) => new RuneLiteral(@Rune);

		/// <summary>
		/// Attempt to consume the <paramref name="pattern"/> from the <paramref name="source"/>.
		/// </summary>
		/// <param name="pattern">The <see cref="Rune"/> to match.</param>
		/// <param name="source">The <see cref="String"/> to consume.</param>
		/// <returns>A <see cref="Result"/> containing whether a match occured and the consumed string.</returns>
		public static Result Consume(this Rune pattern, String source) {
			Guard.NotNull(source, nameof(source));
			return Consume(pattern, source, null);
		}

		/// <summary>
		/// Attempt to consume the <paramref name="pattern"/> from the <paramref name="source"/>.
		/// </summary>
		/// <param name="pattern">The <see cref="Rune"/> to match.</param>
		/// <param name="source">The <see cref="String"/> to consume.</param>
		/// <param name="trace">The <see cref="ITrace"/> to record steps in.</param>
		/// <returns>A <see cref="Result"/> containing whether a match occured and the consumed string.</returns>
		public static Result Consume(this Rune pattern, String source, ITrace? trace) {
			Guard.NotNull(source, nameof(source));
			return pattern.Consume(source, Case.Sensitive, trace);
		}

		/// <summary>
		/// Attempt to consume the <paramref name="pattern"/> from the <paramref name="source"/>.
		/// </summary>
		/// <param name="pattern">The <see cref="Rune"/> to match.</param>
		/// <param name="source">The <see cref="Source"/> to consume.</param>
		/// <param name="comparisonType">Whether the comparison is sensitive to casing.</param>
		/// <returns>A <see cref="Result"/> containing whether a match occured and the consumed string.</returns>
		public static Result Consume(this Rune pattern, String source, Case comparisonType) {
			Guard.NotNull(source, nameof(source));
			return Consume(pattern, source, comparisonType, null);
		}

		/// <summary>
		/// Attempt to consume the <paramref name="pattern"/> from the <paramref name="source"/>.
		/// </summary>
		/// <param name="pattern">The <see cref="Rune"/> to match.</param>
		/// <param name="source">The <see cref="Source"/> to consume.</param>
		/// <param name="comparisonType">Whether the comparison is sensitive to casing.</param>
		/// <param name="trace">The <see cref="ITrace"/> to record steps in.</param>
		/// <returns>A <see cref="Result"/> containing whether a match occured and the consumed string.</returns>
		public static Result Consume(this Rune pattern, String source, Case comparisonType, ITrace? trace) {
			Guard.NotNull(source, nameof(source));
			Source src = new Source(source);
			return pattern.Consume(ref src, comparisonType, trace);
		}

		/// <summary>
		/// Attempt to consume the <paramref name="pattern"/> from the <paramref name="source"/>, adjusting the position in the <paramref name="source"/> as appropriate.
		/// </summary>
		/// <param name="pattern">The <see cref="Rune"/> to match.</param>
		/// <param name="source">The <see cref="Source"/> to consume.</param>
		/// <returns>A <see cref="Result"/> containing whether a match occured and the consumed string.</returns>
		public static Result Consume(this Rune pattern, ref Source source) => pattern.Consume(ref source, Case.Sensitive, null);

		/// <summary>
		/// Attempt to consume the <paramref name="pattern"/> from the <paramref name="source"/>, adjusting the position in the <paramref name="source"/> as appropriate.
		/// </summary>
		/// <param name="pattern">The <see cref="Rune"/> to match.</param>
		/// <param name="source">The <see cref="Source"/> to consume.</param>
		/// <param name="trace">The <see cref="ITrace"/> to record steps in.</param>
		/// <returns>A <see cref="Result"/> containing whether a match occured and the consumed string.</returns>
		public static Result Consume(this Rune pattern, ref Source source, ITrace? trace) => pattern.Consume(ref source, Case.Sensitive, trace);

		/// <summary>
		/// Attempt to consume the <paramref name="pattern"/> from the <paramref name="source"/>, adjusting the position in the <paramref name="source"/> as appropriate.
		/// </summary>
		/// <param name="pattern">The <see cref="Rune"/> to match.</param>
		/// <param name="source">The <see cref="Source"/> to consume.</param>
		/// <param name="comparisonType">Whether the comparison is sensitive to casing.</param>
		/// <returns>A <see cref="Result"/> containing whether a match occured and the consumed string.</returns>
		public static Result Consume(this Rune pattern, ref Source source, Case comparisonType) => Consume(pattern, ref source, comparisonType, null);

		/// <summary>
		/// Attempt to consume the <paramref name="pattern"/> from the <paramref name="source"/>, adjusting the position in the <paramref name="source"/> as appropriate.
		/// </summary>
		/// <param name="pattern">The <see cref="Rune"/> to match.</param>
		/// <param name="source">The <see cref="Source"/> to consume.</param>
		/// <param name="comparisonType">Whether the comparison is sensitive to casing.</param>
		/// <param name="trace">The <see cref="ITrace"/> to record steps in.</param>
		/// <returns>A <see cref="Result"/> containing whether a match occured and the consumed string.</returns>
		public static Result Consume(this Rune pattern, ref Source source, Case comparisonType, ITrace? trace) {
			Result result = new Result(ref source);
			pattern.Consume(ref source, ref result, comparisonType, trace);
			return result;
		}

		/// <summary>
		/// Declares <paramref name="other"/> to be an alternate of this <see cref="Rune"/>.
		/// </summary>
		/// <param name="other">The <see cref="Pattern"/> to check if this <see cref="Rune"/> does not match</param>
		/// <returns>A new <see cref="Pattern"/> alternating this <see cref="Rune"/> and <paramref name="other"/></returns>
		public static Pattern Or(this Rune @Rune, Pattern other) {
			Guard.NotNull(other, nameof(other));
			return new RuneLiteral(@Rune).Alternate(other);
		}

		/// <summary>
		/// Declares <paramref name="other"/> to be an alternate of this <see cref="Rune"/>.
		/// </summary>
		/// <param name="other">The <see cref="String"/> to check if this <see cref="Rune"/> does not match</param>
		/// <returns>A new <see cref="Pattern"/> alternating this <see cref="Rune"/> and <paramref name="other"/></returns>
		public static Pattern Or(this Rune @Rune, String other) {
			Guard.NotNull(other, nameof(other));
			return new RuneLiteral(@Rune).Alternate(other);
		}

		/// <summary>
		/// Declares <paramref name="other"/> to be an alternate of this <see cref="Rune"/>.
		/// </summary>
		/// <param name="other">The <see cref="ReadOnlySpan{T}"/> of <see cref="Char"/> to check if this <see cref="Rune"/> does not match</param>
		/// <returns>A new <see cref="Pattern"/> alternating this <see cref="Rune"/> and <paramref name="other"/></returns>
		public static Pattern Or(this Rune @Rune, ReadOnlySpan<Char> other) => new RuneLiteral(@Rune).Alternate(other);

		/// <summary>
		/// Declares <paramref name="other"/> to be an alternate of this <see cref="Rune"/>.
		/// </summary>
		/// <param name="other">The <see cref="Rune"/> to check if this <see cref="Rune"/> does not match</param>
		/// <returns>A new <see cref="Pattern"/> alternating this <see cref="Rune"/> and <paramref name="other"/></returns>
		public static Pattern Or(this Rune @Rune, Rune other) => new RuneLiteral(@Rune).Alternate(other);

		/// <summary>
		/// Declares <paramref name="other"/> to be an alternate of this <see cref="Rune"/>.
		/// </summary>
		/// <param name="other">The <see cref="Capture"/> to check if this <see cref="Rune"/> does not match</param>
		/// <returns>A new <see cref="Pattern"/> alternating this <see cref="Rune"/> and <paramref name="other"/></returns>
		public static Pattern Or(this Rune @Rune, Capture other) {
			Guard.NotNull(other, nameof(other));
			return new RuneLiteral(@Rune).Alternate(new CaptureLiteral(other));
		}

		/// <summary>
		/// Marks this <see cref="Rune"/> as repeating <paramref name="count"/> times.
		/// </summary>
		/// <param name="count">The amount of times to repeat.</param>
		/// <returns>A new <see cref="Pattern"/> repeated <paramref name="count"/> times.</returns>
		public static Pattern Repeat(this Rune @Rune, Int32 count) => new StringLiteral(StringierExtensions.Repeat(@Rune, count));

		/// <summary>
		/// Concatenates the patterns so that this <see cref="Rune"/> comes before <paramref name="other"/>
		/// </summary>
		/// <param name="other">The succeeding <see cref="Pattern"/></param>
		/// <returns>A new <see cref="Pattern"/> concatenating this <see cref="Rune"/> and <paramref name="other"/></returns>
		public static Pattern Then(this Rune @Rune, Pattern other) {
			Guard.NotNull(other, nameof(other));
			return new RuneLiteral(@Rune).Concatenate(other);
		}

		/// <summary>
		/// Concatenates the patterns so that this <see cref="Rune"/> comes before <paramref name="other"/>
		/// </summary>
		/// <param name="other">The succeeding <see cref="String"/></param>
		/// <returns>A new <see cref="Pattern"/> concatenating this <see cref="Rune"/> and <paramref name="other"/></returns>
		public static Pattern Then(this Rune @Rune, String other) {
			Guard.NotNull(other, nameof(other));
			return new RuneLiteral(@Rune).Concatenate(other);
		}

		/// <summary>
		/// Concatenates the patterns so that this <see cref="Rune"/> comes before <paramref name="other"/>
		/// </summary>
		/// <param name="other">The succeeding <see cref="ReadOnlySpan{T}"/> of <see cref="Char"/>.</param>
		/// <returns>A new <see cref="Pattern"/> concatenating this <see cref="Rune"/> and <paramref name="other"/></returns>
		public static Pattern Then(this Rune @Rune, ReadOnlySpan<Char> other) => new RuneLiteral(@Rune).Concatenate(other);

		/// <summary>
		/// Concatenates the patterns so that this <see cref="Rune"/> comes before <paramref name="other"/>
		/// </summary>
		/// <param name="other">The succeeding <see cref="Rune"/></param>
		/// <returns>A new <see cref="Pattern"/> concatenating this <see cref="Rune"/> and <paramref name="other"/></returns>
		public static Pattern Then(this Rune @Rune, Rune other) => new RuneLiteral(@Rune).Concatenate(other);

		/// <summary>
		/// Concatenates the patterns so that this <see cref="Rune"/> comes before <paramref name="other"/>
		/// </summary>
		/// <param name="other">The succeeding <see cref="Capture"/></param>
		/// <returns>A new <see cref="Pattern"/> concatenating this <see cref="Rune"/> and <paramref name="other"/></returns>
		public static Pattern Then(this Rune @Rune, Capture other) {
			Guard.NotNull(other, nameof(other));
			return new RuneLiteral(@Rune).Concatenate(new CaptureLiteral(other));
		}

		/// <summary>
		/// Compare this <paramref name="pattern"/> with the given <paramref name="comparisonType"/>.
		/// </summary>
		/// <param name="pattern">The <see cref="Rune"/> pattern.</param>
		/// <param name="comparisonType">Whether the comparison is sensitive to casing.</param>
		/// <returns>A new <see cref="Pattern"/> representing the <paramref name="pattern"/> compared with <paramref name="comparisonType"/>.</returns>
		public static Pattern With(this Rune pattern, Case comparisonType) => new RuneLiteral(pattern, comparisonType);

		/// <summary>
		/// Checks the first Runeacter in the <paramref name="source"/> against this Runeacter.
		/// </summary>
		/// <remarks>
		/// This is primarily used to check whether a pattern may exist at the current position.
		/// </remarks>
		/// <param name="source">The <see cref="Source"/> to check against</param>
		/// <returns><c>Compare.CaseSensitive</c> if this <paramref name="pattern"/> may be present, <c>false</c> if definately not.</returns>
		internal static Boolean CheckHeader(this Rune pattern, ref Source source) => !source.EOF && pattern.Equals(source.PeekRune(out Int32 _));

		/// <summary>
		/// Attempt to consume the <paramref name="pattern" />, adjusting the <paramref name="source"/> and <paramref name="result"/> as appropriate.
		/// </summary>
		/// <param name="pattern">The <see cref="Rune"/> pattern.</param>
		/// <param name="source">The <see cref="Source"/> to consume from.</param>
		/// <param name="result">The <see cref="Result"/> to store the result into.</param>
		internal static void Consume(this Rune pattern, ref Source source, ref Result result) => pattern.Consume(ref source, ref result, Case.Sensitive, null);

		/// <summary>
		/// Attempt to consume the <paramref name="pattern" />, adjusting the <paramref name="source"/> and <paramref name="result"/> as appropriate.
		/// </summary>
		/// <param name="pattern">The <see cref="Rune"/> pattern.</param>
		/// <param name="source">The <see cref="Source"/> to consume from.</param>
		/// <param name="result">The <see cref="Result"/> to store the result into.</param>
		/// <param name="comparisonType">Whether the comparison is sensitive to casing.</param>
		/// <param name="trace">The <see cref="ITrace"/> to record steps in.</param>
		internal static void Consume(this Rune pattern, ref Source source, ref Result result, Case comparisonType, ITrace? trace) {
			if (source.Length == 0) {
				result.Error = Error.EndOfSource;
				trace?.Collect(result.Error, source.Position);
			} else {
				Rune Peek = source.PeekRune(out Int32 cons);
				if (pattern.Equals(Peek, comparisonType)) {
					trace?.Collect(Peek, source.Position);
					source.Position += cons;
					result.Length += cons;
					result.Error = Error.None;
				} else {
					result.Error = Error.ConsumeFailed;
					trace?.Collect(result.Error, source.Position);
				}
			}
		}

		/// <summary>
		/// Determines whether this instance and the specified <see cref="Rune"/> have the same value.
		/// </summary>
		/// <param name="rune">This <see cref="Rune"/> instance.</param>
		/// <param name="other">The <see cref="Rune"/> to compare to this instance.</param>
		/// <param name="comparisonType">Whether the comparison is sensitive to casing.</param>
		/// <returns><c>Compare.CaseSensitive</c> if the value of the <paramref name="rune"/> parameter is the same as this Rune; otherwise, <c>false</c>.</returns>
		internal static Boolean Equals(this Rune rune, Rune other, Case comparisonType) {
			switch (comparisonType) {
			case Case.NoPreference:
			case Case.Sensitive:
				return rune.Equals(other, StringComparison.Ordinal);
			case Case.Insensitive:
				return rune.Equals(other, StringComparison.OrdinalIgnoreCase);
			default:
				throw new ArgumentException($"{comparisonType} not handled", nameof(comparisonType));
			}
		}

		/// <summary>
		/// Attempt to neglect the <paramref name="pattern" />, adjusting the <paramref name="source"/> and <paramref name="result"/> as appropriate.
		/// </summary>
		/// <param name="pattern">The <see cref="Rune"/> pattern.</param>
		/// <param name="source">The <see cref="Source"/> to neglect from.</param>
		/// <param name="result">The <see cref="Result"/> to store the result into.</param>
		internal static void Neglect(this Rune pattern, ref Source source, ref Result result) => pattern.Neglect(ref source, ref result, Case.Sensitive, null);

		/// <summary>
		/// Attempt to neglect the <paramref name="pattern" />, adjusting the <paramref name="source"/> and <paramref name="result"/> as appropriate.
		/// </summary>
		/// <param name="pattern">The <see cref="Rune"/> pattern.</param>
		/// <param name="source">The <see cref="Source"/> to neglect from.</param>
		/// <param name="result">The <see cref="Result"/> to store the result into.</param>
		/// <param name="comparisonType">Whether the comparison is sensitive to casing.</param>
		/// <param name="trace">The <see cref="ITrace"/> to record steps in.</param>
		internal static void Neglect(this Rune pattern, ref Source source, ref Result result, Case comparisonType, ITrace? trace) {
			if (source.Length == 0) {
				result.Error = Error.EndOfSource;
				trace?.Collect(result.Error, source.Position);
			} else {
				Rune Peek = source.PeekRune(out Int32 cons);
				if (!pattern.Equals(Peek, comparisonType)) {
					trace?.Collect(Peek, source.Position);
					source.Position += cons;
					result.Length += cons;
					result.Error = Error.None;
				} else {
					result.Error = Error.NeglectFailed;
					trace?.Collect(result.Error, source.Position);
				}
			}
		}
	}
}
