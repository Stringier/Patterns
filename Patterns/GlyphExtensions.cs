using Stringier;
using static Stringier.Patterns.Pattern;

namespace Stringier.Patterns {
	public static class GlyphExtensions {
		/// <summary>
		/// Get the <paramref name="glyph"/> as a <see cref="Pattern"/>.
		/// </summary>
		/// <param name="glyph">The <see cref="Glyph"/> literal.</param>
		/// <returns>A <see cref="Pattern"/> representing literally the <paramref name="glyph"/>.</returns>
		public static Pattern AsPattern(this Glyph glyph) => OneOf(Compare.None, Glyph.GetVariants(glyph));
	}
}
