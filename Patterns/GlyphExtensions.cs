using Stringier;
using static Stringier.Patterns.Pattern;

namespace Stringier.Patterns {
	public static class GlyphExtensions {
		/// <summary>
		/// Get the <paramref name="glyph"/> as a <see cref="Pattern"/>.
		/// </summary>
		/// <param name="glyph">The <see cref="Glyph"/> literal.</param>
		/// <returns>A <see cref="Pattern"/> representing literally the <paramref name="glyph"/>.</returns>
		public static Pattern AsPattern(this Glyph glyph) => OneOf(Case.NoPreference, Glyph.GetVariants(glyph));

		/// <summary>
		/// Compare this <paramref name="pattern"/> with the given <paramref name="comparisonType"/>.
		/// </summary>
		/// <param name="pattern">The <see cref="Glyph"/> pattern.</param>
		/// <param name="comparisonType">Whether the comparison is sensitive to casing.</param>
		/// <returns>A new <see cref="Pattern"/> representing the <paramref name="pattern"/> compared with <paramref name="comparisonType"/>.</returns>
		public static Pattern With(this Glyph pattern, Case comparisonType) => OneOf(comparisonType, Glyph.GetVariants(pattern));
	}
}
