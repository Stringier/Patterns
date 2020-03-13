namespace Stringier.Patterns {
	/// <summary>
	/// The grapheme comparison mode to use when parsing.
	/// </summary>
	public enum Grapheme {
		/// <summary>
		/// Comparisons should be done with regards to grapheme encoding.
		/// </summary>
		Sensitive,

		/// <summary>
		/// Comparisons should be done without regards to grapheme encoding.
		/// </summary>
		Insensitive,
	}
}
