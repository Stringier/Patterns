using System;

namespace Stringier.Patterns.Debugging {
	/// <summary>
	/// Provides convenience extensions for <see cref="Error"/>.
	/// </summary>
	internal static class ErrorExtensions {
		/// <summary>
		/// Throw the <see cref="ParserException"/> corresponding to the <paramref name="error"/> value.
		/// </summary>
		/// <param name="error">The <see cref="Error"/> value to throw.</param>
		internal static void Throw(this Error error) {
			switch (error) {
			case Error.None:
				return;
			case Error.ConsumeFailed:
				throw new ConsumeFailedException($"Consume failed.");
			case Error.EndOfSource:
				throw new EndOfSourceException($"End of source has been reached before able to parse.");
			case Error.NeglectFailed:
				throw new NeglectFailedException($"Neglect failed.");
			default:
				throw new NotSupportedException($"{error} doesn't have a conversion.");
			}
		}
	}
}
