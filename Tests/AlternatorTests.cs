using System;
using Stringier.Patterns;
using static Stringier.Patterns.Pattern;
using Defender;
using Xunit;
using FParsec.CSharp;
using Pidgin;
using Sprache;
using Benchmarks;
using static Benchmarks.AlternatorBenchmarks;

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

		[Theory]
		[InlineData("Hello", true, "Hello")]
		[InlineData("Goodbye", true, "Goodbye")]
		[InlineData("Failure", false, null)]
		public void FParsec(String source, Boolean success, String expected) {
			var result = fparsec.Alternator.RunOnString(source);
			Assert.Equal(success, result.IsSuccess);
			if (success) {
				Assert.Equal(expected, result.GetResult());
			}
		}

		[Theory]
		[InlineData("Hello", true, "Hello")]
		[InlineData("Goodbye", true, "Goodbye")]
		[InlineData("Failure", false, null)]
		public void Pidgin(String source, Boolean success, String expected) {
			Result<Char, String> result = pidgin.Parse(source);
			Assert.Equal(success, result.Success);
			if (success) {
				Assert.Equal(expected, result.Value);
			}
		}

		[Theory]
		[InlineData("Hello", true, "Hello")]
		[InlineData("Goodbye", true, "Goodbye")]
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
