using System;
using Stringier.Patterns;
using static Stringier.Patterns.Pattern;
using Defender;
using Xunit;

namespace Tests {
	public class SourceStateTests : Trial {
		[Fact]
		public void StoreAndRestore() {
			Source source = new Source("hello world");
			SourceState state = source.Store();
			Claim.That("hello").Consumes("hello", ref source);
			source.Restore(state);
			Claim.That("hello").Consumes("hello", ref source);
		}
	}
}
