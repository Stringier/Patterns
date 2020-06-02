using System;

namespace Stringier.Patterns.Debugging {
	/// <summary>
	/// Represents a <see cref="ITrace"/> step; a snapshot of execution.
	/// </summary>
	public interface IStep {
		/// <summary>
		/// The <see cref="Debugging.Error"/> of this step.
		/// </summary>
		public Error Error { get; }

		/// <summary>
		/// The position within the source where this step began.
		/// </summary>
		public Int32 Position { get; }

		/// <summary>
		/// The text consumed as part of this step, if any.
		/// </summary>
		public String? Text { get; }
	}
}
