namespace Stringier.Patterns.Debugging {
	/// <summary>
	/// Specifies the type of error that occured.
	/// </summary>
	/// <remarks>
	/// This is used to map to the equivalent <see cref="System.Exception"/> when the <see cref="Error"/> is thrown.
	/// </remarks>
	public enum Error {
		/// <summary>
		/// No error.
		/// </summary>
		None = 0,

		/// <summary>
		/// A consume parser failed.
		/// </summary>
		ConsumeFailed,

		/// <summary>
		/// A neglect parser failed.
		/// </summary>
		NeglectFailed,

		/// <summary>
		/// The end of the source was reached when still trying to parse.
		/// </summary>
		EndOfSource,
	}
}
