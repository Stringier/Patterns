namespace Stringier.Patterns {
	/// <summary>
	/// The case comparison mode to use when parsing.
	/// </summary>
	public enum Case {
		/// <summary>
		/// No comparison preference (default behavior is <see cref="Sensitive"/>).
		/// </summary>
		/// <remarks>
		/// This is intended to specify "I don't care", or default behavior.
		/// </remarks>
		NoPreference,

		/// <summary>
		/// Comparisons should be done with regards to letter casing.
		/// </summary>
		Sensitive,

		/// <summary>
		/// Comparisons should be done without regards to letter casing.
		/// </summary>
		Insensitive,
	}
}
