using System;

namespace Stringier.Patterns {
	/// <summary>
	/// The comparison mode to use when parsing.
	/// </summary>
	[Flags]
	public enum Compare : Byte {
		/// <summary>
		/// No comparison preference (default behavior is <see cref="CaseSensitive"/>, <see cref="GlyphSensitive"/>).
		/// </summary>
		/// <remarks>
		/// This is intended to specify "I don't care", or default behavior.
		/// </remarks>
		None = 0b0000_0000,

		/// <summary>
		/// Comparisons should be done with regards to letter casing.
		/// </summary>
		CaseSensitive = 0b0000_0001,

		/// <summary>
		/// Comparisons should be done without regards to letter casing.
		/// </summary>
		CaseInsensitive = 0b0000_0010,

		/// <summary>
		/// Comparisons don't have a casing preference.
		/// </summary>
		NoCasePreference = 0b0000_0011,

		/// <summary>
		/// Comparisons should be done with regards to grapheme encoding.
		/// </summary>
		GlyphSensitive = 0b0001_0000,

		/// <summary>
		/// Comparisons should be done without regards to grapheme encoding.
		/// </summary>
		GlyphInsensitive = 0b0010_0000,

		/// <summary>
		/// Comparisons don't have a grapheme preference.
		/// </summary>
		NoGlyphPreference = 0b0011_0000,
	}
}
