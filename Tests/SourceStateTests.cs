using System;
using Stringier.Patterns;
using static Stringier.Patterns.Pattern;
using Xunit;

namespace Tests {
	public class SourceStateTests {
		[Fact]
		public void StoreAndRestore() {
			Source source = new Source("hello world");
			SourceState state = source.Store();
			ResultAssert.Captures("hello", "hello".Consume(ref source));
			source.Restore(state);
			ResultAssert.Captures("hello", "hello".Consume(ref source));
		}
	}
}
