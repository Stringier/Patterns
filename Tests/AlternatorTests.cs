using System;
using Stringier.Patterns;
using static Stringier.Patterns.Pattern;
using Defender;
using Xunit;

namespace Tests {
	public class AlternatorTests : Trial {
		[Fact]
		public void Constructor() => "Hello".Or("Goodbye");

		public enum Enum { First, Second, Third }

		[Fact]
		public void Consume() {
			Pattern pattern = "Hello".Or("Goodbye");
			Claim.That(pattern)
				.Consumes("Hello", "Hello")
				.Consumes("Goodbye", "Goodbye")
				.FailsToConsume("!")
				.FailsToConsume("How are you?");

			pattern = "Hello".Or("Hi").Or("Howdy");
			Claim.That(pattern)
				.Consumes("Hello", "Hello")
				.Consumes("Hi", "Hi")
				.Consumes("Howdy", "Howdy")
				.FailsToConsume("Goodbye");

			pattern = OneOf("Hello", "Hi", "Howdy");
			Claim.That(pattern)
				.Consumes("Hello", "Hello")
				.Consumes("Hi", "Hi")
				.Consumes("Howdy", "Howdy")
				.FailsToConsume("Goodbye");

			pattern = OneOf<Enum>();
			Claim.That(pattern)
				.Consumes("First", "First")
				.Consumes("Second", "Second")
				.Consumes("Third", "Third")
				.FailsToConsume("Fourth");
		}

		[Fact]
		public void Neglect() {
			Pattern pattern = Not("Hello".Or("Goodbye"));
			Claim.That(pattern)
				.Consumes("World", "Worldeater")
				.FailsToConsume("Hello")
				.FailsToConsume("Goodbye");
		}
	}
}
