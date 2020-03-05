using System;
using Stringier.Patterns;
using static Stringier.Patterns.Pattern;
using Defender;
using Xunit;

namespace Tests {
	public class ConcatenatorTests : Trial {
		[Fact]
		public void Constructor() => "Hello".Then(' ').Then("world");

		[Fact]
		public void Consume() {
			Claim.That("Hello".Then(' ').Then("world"))
				.Consumes("Hello world", "Hello world")
				.FailsToConsume("Goodbye world");
			Claim.That("Goodbye".Then(' ').Then("world"))
				.Consumes("Goodbye world", "Goodbye world")
				.FailsToConsume("Hello world");
		}

		[Fact]
		public void Neglect() {
			Claim.That(Not("Hello".Then('!')))
				.Consumes("Hello.", "Hello.")
				.Consumes("Oh no!", "Oh no!")
				.Consumes("Oh no?", "Oh no?")
				.FailsToConsume("Hello")
				.FailsToConsume("Hello!");
		}
	}
}
