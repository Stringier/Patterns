namespace Stringier.Patterns.Nodes {
	/// <summary>
	/// Represents any possible literal, a <see cref="Pattern"/> representing exactly itself.
	/// </summary>
	internal abstract class Literal : Primative {
		/// <summary>
		/// The <see cref="Case"/> to use when parsing.
		/// </summary>
		internal readonly Case ComparisonType;

		/// <summary>
		/// Initialize a new <see cref="Literal"/> with the default <see cref="Case"/> (<see cref="Case.NoPreference"/>).
		/// </summary>
		protected Literal() => ComparisonType = Case.NoPreference;

		/// <summary>
		/// Intialize a new <see cref="Literal"/> with the given <paramref name="comparisonType"/>.
		/// </summary>
		/// <param name="comparisonType">The <see cref="Case"/> to use when parsing.</param>
		protected Literal(Case comparisonType) => ComparisonType = comparisonType;
	}
}
