using System;
using Stringier.Patterns;
using Xunit.Sdk;

namespace Defender {
	/// <summary>
	/// A collection of extension methods to test various conditions associated with <see cref="Stringier.Patterns"/> types within unit tests.
	/// </summary>
	public static class PatternsExtensions {
		/// <summary>
		/// Tests whether the specified <paramref name="claim"/> capture is what was <paramref name="expected"/>.
		/// </summary>
		/// <param name="claim">The <see cref="Claim{T}"/> of <see cref="Pattern"/> object.</param>
		/// <param name="expected">The <see cref="String"/> of expected text.</param>
		public static Claim<Capture> Captures(this Claim<Capture> claim, String expected) {
			Guard.NotNull(expected, nameof(expected));
			if (!claim.Value.Equals(expected)) {
				throw new EqualException(expected, claim.Value);
			}
			return claim;
		}

		/// <summary>
		/// Tests whether the specified <paramref name="claim"/> consumes what was <paramref name="expected"/>.
		/// </summary>
		/// <param name="claim">The <see cref="Claim{T}"/> of <see cref="Pattern"/> object.</param>
		/// <param name="source">The <see cref="Source"/> text.</param>
		/// <param name="expected">The <see cref="String"/> of expected text.</param>
		public static Claim<Pattern> Consumes(this Claim<Pattern> claim, String expected, ref Source source) {
			Guard.NotNull(expected, nameof(expected));
			Result result = claim.Value.Consume(ref source);
			result.ThrowException();
			if (expected != result) {
				throw new EqualException(expected, result.ToString());
			}
			return claim;
		}

		/// <summary>
		/// Tests whether the specified <paramref name="claim"/> consumes what was <paramref name="expected"/>.
		/// </summary>
		/// <param name="claim">The <see cref="Claim{T}"/> of <see cref="Pattern"/> object.</param>
		/// <param name="source">The source text.</param>
		/// <param name="expected">The <see cref="String"/> of expected text.</param>
		public static Claim<Pattern> Consumes(this Claim<Pattern> claim, String expected, String source) {
			Guard.NotNull(expected, nameof(expected));
			Source src = new Source(source);
			Result result = claim.Value.Consume(ref src);
			result.ThrowException();
			if (expected != result) {
				throw new EqualException(expected, result.ToString());
			}
			return claim;
		}

		/// <summary>
		/// Tests whether the specified <paramref name="claim"/> consumes what was <paramref name="expected"/>.
		/// </summary>
		/// <param name="claim">The <see cref="Claim{T}"/> of <see cref="String"/> object.</param>
		/// <param name="source">The <see cref="Source"/> text.</param>
		/// <param name="expected">The <see cref="String"/> of expected text.</param>
		public static Claim<String> Consumes(this Claim<String> claim, String expected, ref Source source) {
			Guard.NotNull(expected, nameof(expected));
			Result result = claim.Value.Consume(ref source);
			result.ThrowException();
			if (expected != result) {
				throw new EqualException(expected, result.ToString());
			}
			return claim;
		}

		/// <summary>
		/// Tests whether the specified <paramref name="claim"/> consumes what was <paramref name="expected"/>.
		/// </summary>
		/// <param name="claim">The <see cref="Claim{T}"/> of <see cref="String"/> object.</param>
		/// <param name="source">The source text.</param>
		/// <param name="expected">The <see cref="String"/> of expected text.</param>
		public static Claim<String> Consumes(this Claim<String> claim, String expected, String source) {
			Guard.NotNull(expected, nameof(expected));
			Source src = new Source(source);
			Result result = claim.Value.Consume(ref src);
			result.ThrowException();
			if (expected != result) {
				throw new EqualException(expected, result.ToString());
			}
			return claim;
		}

		/// <summary>
		/// Tests whether the parse failed.
		/// </summary>
		/// <param name="claim">The <see cref="Claim{T}"/> of <see cref="Pattern"/> object.</param>
		/// <param name="source">The <see cref="Source"/> text.</param>
		public static Claim<Pattern> FailsToConsume(this Claim<Pattern> claim, ref Source source) {
			Result result = claim.Value.Consume(ref source);
			if (result) {
				throw new FalseException("Parser succeeded when expected to fail", result);
			}
			return claim;
		}

		/// <summary>
		/// Tests whether the parse failed.
		/// </summary>
		/// <param name="claim">The <see cref="Claim{T}"/> of <see cref="Pattern"/> object.</param>
		/// <param name="source">The source text.</param>
		public static Claim<Pattern> FailsToConsume(this Claim<Pattern> claim, String source) {
			Source src = new Source(source);
			Result result = claim.Value.Consume(ref src);
			if (result) {
				throw new FalseException("Parser succeeded when expected to fail", result);
			}
			return claim;
		}

		/// <summary>
		/// Tests whether the parse failed.
		/// </summary>
		/// <param name="claim">The <see cref="Claim{T}"/> of <see cref="String"/> object.</param>
		/// <param name="source">The <see cref="Source"/> text.</param>
		public static Claim<String> FailsToConsume(this Claim<String> claim, ref Source source) {
			Result result = claim.Value.Consume(ref source);
			if (result) {
				throw new FalseException("Parser succeeded when expected to fail", result);
			}
			return claim;
		}

		/// <summary>
		/// Tests whether the parse failed.
		/// </summary>
		/// <param name="claim">The <see cref="Claim{T}"/> of <see cref="String"/> object.</param>
		/// <param name="source">The source text.</param>
		public static Claim<String> FailsToConsume(this Claim<String> claim, String source) {
			Source src = new Source(source);
			Result result = claim.Value.Consume(ref src);
			if (result) {
				throw new FalseException("Parser succeeded when expected to fail", result);
			}
			return claim;
		}

		/// <summary>
		/// Tests whether the parse succeeded.
		/// </summary>
		/// <param name="claim">The <see cref="Claim{T}"/> of <see cref="Pattern"/> object.</param>
		/// <param name="source">The <see cref="Source"/> text.</param>
		public static Claim<Pattern> SucceedsToConsume(this Claim<Pattern> claim, ref Source source) {
			Result result = claim.Value.Consume(ref source);
			result.ThrowException();
			return claim;
		}

		/// <summary>
		/// Tests whether the parse succeeded.
		/// </summary>
		/// <param name="claim">The <see cref="Claim{T}"/> of <see cref="Pattern"/> object.</param>
		/// <param name="source">The source text.</param>
		public static Claim<Pattern> SucceedsToConsume(this Claim<Pattern> claim, String source) {
			Source src = new Source(source);
			Result result = claim.Value.Consume(ref src);
			result.ThrowException();
			return claim;
		}

		/// <summary>
		/// Tests whether the parse succeeded.
		/// </summary>
		/// <param name="claim">The <see cref="Claim{T}"/> of <see cref="String"/> object.</param>
		/// <param name="source">The <see cref="Source"/> text.</param>
		public static Claim<String> SucceedsToConsume(this Claim<String> claim, ref Source source) {
			Result result = claim.Value.Consume(ref source);
			result.ThrowException();
			return claim;
		}

		/// <summary>
		/// Tests whether the parse succeeded.
		/// </summary>
		/// <param name="claim">The <see cref="Claim{T}"/> of <see cref="String"/> object.</param>
		/// <param name="source">The source text.</param>
		public static Claim<String> SucceedsToConsume(this Claim<String> claim, String source) {
			Source src = new Source(source);
			Result result = claim.Value.Consume(ref src);
			result.ThrowException();
			return claim;
		}
	}
}
