using System;
using Stringier.Patterns;
using static Stringier.Patterns.Pattern;
using Defender;
using Xunit;

namespace Tests {
	public class FuzzerTests : Trial {
		[Fact]
		public void Constructor() => Fuzzy("Bob");

		[Fact]
		public void Consume() {
			Claim.That(Fuzzy("Bob"))
				.Consumes("Bob", "Bob")
				.Consumes("bob", "bob")
				.Consumes("Mob", "Mob")
				.Consumes("mob", "mob")
				.FailsToConsume("Mom");
		}
	}
}
