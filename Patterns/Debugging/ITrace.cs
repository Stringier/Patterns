﻿using System;
using System.Collections.Generic;

namespace Stringier.Patterns.Debugging {
	/// <summary>
	/// Represents a graph trace, a collection of the steps taken through a <see cref="Pattern"/> graph.
	/// </summary>
	public interface ITrace : IEnumerable<IStep> {
		/// <summary>
		/// Gets the <see cref="IStep"/> at the declared index <paramref name="i"/>.
		/// </summary>
		/// <param name="i">The index of the <see cref="IStep"/> to get.</param>
		/// <returns>The <see cref="IStep"/> at the declared index <paramref name="i"/>.</returns>
		public IStep this[Int32 i] { get; }

		/// <summary>
		/// Collect the parameters as a trace step.
		/// </summary>
		/// <param name="text">The text consumed.</param>
		/// <param name="position">The position at which <paramref name="text"/> began.</param>
		public void Collect(String text, Int32 position);

		/// <summary>
		/// Collect the parameters as a trace step.
		/// </summary>
		/// <param name="error">The parser <see cref="Debugging.Error"/>.</param>
		/// <param name="position">The position at which the parser failed.</param>
		public void Collect(Error error, Int32 position);
	}
}
