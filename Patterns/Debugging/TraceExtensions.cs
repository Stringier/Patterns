using System;
using System.Text;

namespace Stringier.Patterns.Debugging {
	/// <summary>
	/// A collection of extensions for <see cref="ITrace"/> to make collecting easier without requiring additional implementations by downstream.
	/// </summary>
	/// <remarks>
	/// While default interface implementations would also solve this problem, doing so requires a sufficiently recent version of the runtime, far newer than <see cref="Stringier"/> targets. Furthermore, these extensions really aren't anything downstream should even be interested in seeing, as they are purely for convenience.
	/// </remarks>
	internal static class TraceExtensions {
		/// <summary>
		/// Collects the parameters as a trace step.
		/// </summary>
		/// <param name="trace">The <see cref="ITrace"/> doing the collection.</param>
		/// <param name="text">The text consumed.</param>
		/// <param name="position">The position at which <paramref name="text"/> began.</param>
		internal static void Collect(this ITrace trace, Char text, Int32 position) => trace.Collect(text.ToString(), position);

		/// <summary>
		/// Collects the parameters as a trace step.
		/// </summary>
		/// <param name="trace">The <see cref="ITrace"/> doing the collection.</param>
		/// <param name="text">The text consumed.</param>
		/// <param name="position">The position at which <paramref name="text"/> began.</param>
		internal static void Collect(this ITrace trace, Rune text, Int32 position) => trace.Collect(text.ToString(), position);

		/// <summary>
		/// Collects the parameters as a trace step.
		/// </summary>
		/// <param name="trace">The <see cref="ITrace"/> doing the collection.</param>
		/// <param name="text">The text consumed.</param>
		/// <param name="position">The position at which <paramref name="text"/> began.</param>
		internal static void Collect(this ITrace trace, ReadOnlySpan<Char> text, Int32 position) => trace.Collect(text.ToString(), position);
	}
}
