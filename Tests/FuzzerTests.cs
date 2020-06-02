using System;
using Stringier.Patterns;
using static Stringier.Patterns.Pattern;
using Xunit;

namespace Tests {
	public class FuzzerTests {
		[Fact]
		public void Constructor() => Fuzzy("Bob");

		[Fact]
		public void Consume() {
			Pattern pattern = Fuzzy("Bob");
			ResultAssert.Captures("Bob", pattern.Consume("Bob"));
			ResultAssert.Captures("bob", pattern.Consume("bob"));
			ResultAssert.Captures("Mob", pattern.Consume("Mob"));
			ResultAssert.Captures("mob", pattern.Consume("mob"));
			ResultAssert.Fails(pattern.Consume("Mom"));
		}
	}
}
