using System;
using Stringier.Patterns;
using static Stringier.Patterns.Pattern;
using Xunit;
using FParsec.CSharp;
using Pidgin;
using Sprache;
using Benchmarks;
using static Benchmarks.ConcatenatorBenchmarks;

namespace Tests {
	public class ConcatenatorTests {
		[Fact]
		public void Constructor() => "Hello".Then(' ').Then("world");

		[Fact]
		public void Consume() {
			Pattern pattern = "Hello".Then(' ').Then("world");
			ResultAssert.Captures("Hello world", pattern.Consume("Hello world"));
			ResultAssert.Fails(pattern.Consume("Goodbye world"));
			pattern = "Goodbye".Then(' ').Then("world");
			ResultAssert.Captures("Goodbye world", pattern.Consume("Goodbye world"));
			ResultAssert.Fails(pattern.Consume("Hello world"));
		}

		[Fact]
		public void Neglect() {
			Pattern pattern = Not("Hello".Then('!'));
			ResultAssert.Captures("Hello.", pattern.Consume("Hello."));
			ResultAssert.Captures("Oh no!", pattern.Consume("Oh no!"));
			ResultAssert.Captures("Oh no?", pattern.Consume("Oh no?"));
			ResultAssert.Fails(pattern.Consume("Hello"));
			ResultAssert.Fails(pattern.Consume("Hello!"));
		}

		[Theory]
		[InlineData("Hello World", true, new[] { "Hello", " ", "World" })]
		[InlineData("Failure", false, null)]
		public void FParsec(String source, Boolean success, String[] expected) {
			var result = fparsec.Concatenator.RunOnString(source);
			Assert.Equal(success, result.IsSuccess);
			if (success) {
				Assert.Equal(expected[0], result.GetResult().Item1.Item1);
				Assert.Equal(expected[1][0], result.GetResult().Item1.Item2);
				Assert.Equal(expected[2], result.GetResult().Item2);
			}
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
