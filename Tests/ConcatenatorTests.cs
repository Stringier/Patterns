using System;
using Stringier.Patterns;
using static Stringier.Patterns.Pattern;
using Defender;
using Xunit;
using Pidgin;
using Sprache;
using static Benchmarks.ConcatenatorBenchmarks;

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

		[Theory]
		[InlineData("Hello World", true, "Hello World")]
		[InlineData("Failure", false, null)]
		public void Pidgin(String source, Boolean success, String expected) {
			Result<Char, String> result = pidgin.Parse(source);
			Assert.Equal(success, result.Success);
			if (success) {
				Assert.Equal(expected, result.Value);
			}
		}

		[Theory]
		[InlineData("Hello World", true, "Hello World")]
		[InlineData("Failure", false, null)]
		public void Sprache(String source, Boolean success, String expected) {
			IResult<String> result = sprache.TryParse(source);
			Assert.Equal(success, result.WasSuccessful);
			if (success) {
				Assert.Equal(expected, result.Value);
			}
		}
	}
}
