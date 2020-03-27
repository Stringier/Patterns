using System;
using Defender;

namespace Stringier.Patterns {
	public abstract partial class Pattern {
		/// <summary>
		/// Creates a pattern representing a line comment introduced by the <paramref name="start"/> and ended by the <paramref name="stop"/> delimiters.
		/// </summary>
		/// <param name="start">The token delimiting the start of a block comment.</param>
		/// <param name="stop">The token delimiting the stop of a block comment.</param>
		/// <returns>A pattern representing a block comment.</returns>
		public static Pattern BlockComment(String start, String stop) {
			Guard.NotNull(start, nameof(start));
			Guard.NotEmpty(start, nameof(start));
			Guard.NotNull(stop, nameof(stop));
			Guard.NotEmpty(stop, nameof(stop));
			return NestedRange(start, stop);
		}

		/// <summary>
		/// Creates a pattern representing a line comment introduced by the <paramref name="delimiter"/>.
		/// </summary>
		/// <param name="delimiter">The token delimiting the start of a line comment.</param>
		/// <returns>A pattern representing a line comment.</returns>
		public static Pattern LineComment(String delimiter) {
			Guard.NotNull(delimiter, nameof(delimiter));
			Guard.NotEmpty(delimiter, nameof(delimiter));
			return delimiter.Then(Many(Not(LineTerminator)));
		}

		/// <summary>
		/// Creates a pattern representing a string literal contained by the <paramref name="delimiter"/>.
		/// </summary>
		/// <param name="delimiter">The token delimiting the start and end of the literal.</param>
		/// <returns>A pattern representing a string literal.</returns>
		public static Pattern StringLiteral(String delimiter) {
			Guard.NotNull(delimiter, nameof(delimiter));
			Guard.NotEmpty(delimiter, nameof(delimiter));
			return Range(delimiter, delimiter);
		}

		/// <summary>
		/// Creates a pattern representing a string literal contained by the <paramref name="delimiter"/>.
		/// </summary>
		/// <param name="delimiter">The token delimiting the start and end of the literal.</param>
		/// <param name="escape">The token escaping the <paramref name="delimiter"/>.</param>
		/// <returns>A pattern representing a string literal.</returns>
		public static Pattern StringLiteral(String delimiter, String escape) {
			// escape parameter not being nullable is not an error. It shouldn't appear as nullable. If you don't want an escape, don't use the parameter. However, we can reasonably handle that case without exceptions, so don't even bother with that mess.
			Guard.NotNull(delimiter, nameof(delimiter));
			Guard.NotNull(delimiter, nameof(delimiter));
			if (escape is null || escape.Length == 0) {
				return Range(delimiter, delimiter);
			} else {
				return Range(delimiter, delimiter, escape);
			}
		}
	}
}
