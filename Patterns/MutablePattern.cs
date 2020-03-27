using System;
using Stringier.Patterns.Debugging;
using Stringier.Patterns.Nodes;
using Defender;

namespace Stringier.Patterns {
	/// <summary>
	/// Represents a textual pattern; this pattern mutates during construction.
	/// </summary>
	/// <remarks>
	/// This is a specialization for use with recursion systems. Because a target is created from a <see cref="Pattern"/> and points to it, the reference must not change, yet the underlying pattern must change. That means the object must mutate, which is generally undesired and avoided.
	/// </remarks>
	internal sealed class MutablePattern : Pattern {
		/// <summary>
		/// The <see cref="Pattern"/> at the head of this <see cref="Pattern"/>; the entry point of the graph.
		/// </summary>
		internal Pattern? Head;

		/// <summary>
		/// Initialize a new <see cref="MutablePattern"/>.
		/// </summary>
		internal MutablePattern() { }

		/// <inheritdoc/>
		public override Pattern Seal() {
			if (Head is null) {
				throw new PatternUndefinedException();
			} else {
				return Head;
			}
		}

		/// <inheritdoc/>
		internal override Boolean CheckHeader(ref Source source) => Head?.CheckHeader(ref source) ?? false;

		/// <inheritdoc/>
		internal override void Consume(ref Source source, ref Result result, ITrace? trace) {
			if (Head is null) {
				throw new PatternUndefinedException();
			}
			Head.Consume(ref source, ref result, trace);
		}

		/// <inheritdoc/>
		internal override void Neglect(ref Source source, ref Result result, ITrace? trace) {
			if (Head is null) {
				throw new PatternUndefinedException();
			}
			Head.Neglect(ref source, ref result, trace);
		}

		#region Alternator

		/// <inheritdoc/>
		internal sealed override Pattern Alternate(Pattern other) {
			Guard.NotNull(other, nameof(other));
			if (Head is null) {
				throw new PatternUndefinedException();
			}
			Head = Head.Alternate(other);
			return this;
		}

		/// <inheritdoc/>
		internal sealed override Pattern Alternate(String other) {
			Guard.NotNull(other, nameof(other));
			if (Head is null) {
				throw new PatternUndefinedException();
			}
			Head = Head.Alternate(other);
			return this;
		}

		/// <inheritdoc/>
		internal sealed override Pattern Alternate(Char other) {
			if (Head is null) {
				throw new PatternUndefinedException();
			}
			Head = Head.Alternate(other);
			return this;
		}

		#endregion

		#region Capturer

		/// <inheritdoc/>
		public sealed override Pattern Capture(out Capture capture) {
			if (Head is null) {
				throw new PatternUndefinedException();
			}
			Head = Head.Capture(out capture);
			return this;
		}

		#endregion

		#region Concatenator

		/// <inheritdoc/>
		internal sealed override Pattern Concatenate(Pattern other) {
			Guard.NotNull(other, nameof(other));
			Head = Head is null ? other : Head.Concatenate(other);
			return this;
		}

		/// <inheritdoc/>
		internal sealed override Pattern Concatenate(String other) {
			Guard.NotNull(other, nameof(other));
			Head = Head is null ? new StringLiteral(other) : Head.Concatenate(other);
			return this;
		}

		/// <inheritdoc/>
		internal sealed override Pattern Concatenate(Char other) {
			Head = Head is null ? new CharLiteral(other) : Head.Concatenate(other);
			return this;
		}

		#endregion Concatenator

		#region Negator

		/// <inheritdoc/>
		internal sealed override Pattern Negate() {
			if (Head is null) {
				throw new PatternUndefinedException();
			}
			Head = Head.Negate();
			return this;
		}

		#endregion

		#region Optor

		/// <inheritdoc/>
		internal sealed override Pattern Optional() {
			if (Head is null) {
				throw new PatternUndefinedException();
			}
			Head = Head.Optional();
			return this;
		}

		#endregion

		#region Repeater

		/// <inheritdoc/>
		public sealed override Pattern Repeat(Int32 count) {
			if (Head is null) {
				throw new PatternUndefinedException();
			}
			Head = Head.Repeat(count);
			return this;
		}

		#endregion

		#region Spanner

		/// <inheritdoc/>
		internal sealed override Pattern Span() {
			if (Head is null) {
				throw new PatternUndefinedException();
			}
			Head = Head.Span();
			return this;
		}

		#endregion
	}
}
