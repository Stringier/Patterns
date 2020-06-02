using System;
using Stringier.Patterns;
using static Stringier.Patterns.Pattern;
using Xunit;
using FParsec.CSharp;
using Pidgin;
using Sprache;
using Benchmarks;
using static Benchmarks.AlternatorBenchmarks;

namespace Tests {
	public class AlternatorTests {
		[Fact]
		public void Constructor() => "Hello".Or("Goodbye");

		public enum Enum { First, Second, Third }

		[Fact]
		public void Consume() {
			Pattern pattern = "Hello".Or("Goodbye");
			ResultAssert.Captures("Hello", pattern.Consume("Hello"));
			ResultAssert.Captures("Goodbye", pattern.Consume("Goodbye"));
			ResultAssert.Fails(pattern.Consume("!"));
			ResultAssert.Fails(pattern.Consume("How are you?"));

			pattern = "Hello".Or("Hi").Or("Howdy");
			ResultAssert.Captures("Hello", pattern.Consume("Hello"));
			ResultAssert.Captures("Hi", pattern.Consume("Hi"));
			ResultAssert.Captures("Howdy", pattern.Consume("Howdy"));
			ResultAssert.Fails(pattern.Consume("Goodbye"));

			pattern = OneOf("Hello", "Hi", "Howdy");
			ResultAssert.Captures("Hello", pattern.Consume("Hello"));
			ResultAssert.Captures("Hi", pattern.Consume("Hi"));
			ResultAssert.Captures("Howdy", pattern.Consume("Howdy"));
			ResultAssert.Fails(pattern.Consume("Goodbye"));

			pattern = OneOf<Enum>();
			ResultAssert.Captures("First", pattern.Consume("First"));
			ResultAssert.Captures("Second", pattern.Consume("Second"));
			ResultAssert.Captures("Third", pattern.Consume("Third"));
			ResultAssert.Fails(pattern.Consume("Fourth"));
		}

		[Fact]
		public void Neglect() {
			Pattern pattern = Not("Hello".Or("Goodbye"));
			ResultAssert.Captures("World", pattern.Consume("Worldeater"));
			ResultAssert.Fails(pattern.Consume("Hello"));
			ResultAssert.Fails(pattern.Consume("Goodbye"));
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
