using System;
using System.Collections.Generic;
using Stringier;
using Stringier.Patterns;
using static Stringier.Patterns.Pattern;
using Xunit;
using Pidgin;
using Sprache;
using static Benchmarks.RepeaterBenchmarks;

namespace Tests {
	public class RepeaterTests {
		// We use .AsPattern() and * instead of .Repeat() here because we had to import both Stringier and Stringier.Patterns which both have .Repeat() overloads which do different things.

		[Fact]
		public void Consume() {
			Pattern pattern = "Hi! ".AsPattern() * 2;
			ResultAssert.Captures("Hi! Hi! ", pattern.Consume("Hi! Hi! "));
			ResultAssert.Captures("Hi! Hi! ", pattern.Consume("Hi! Hi! Hi!"));
			ResultAssert.Fails(pattern.Consume("Hi! Bye! "));
		}

		[Fact]
		public void Neglect() {
			Pattern pattern = Not("Hi! ".AsPattern() * 2);
			ResultAssert.Captures("Oh! No! ", pattern.Consume("Oh! No! "));
			ResultAssert.Captures("Oh? Hi! ", pattern.Consume("Oh? Hi! "));
			ResultAssert.Captures("Hi! Ho! ", pattern.Consume("Hi! Ho! "));
			ResultAssert.Fails(pattern.Consume("H! Hi! "));
		}

		[Theory]
		[InlineData("Hi!", false, null)]
		[InlineData("Hi!Hi!Hi!Hi!Hi!", true, "Hi!Hi!Hi!Hi!Hi!")]
		public void Pidgin(String source, Boolean success, String expected) {
			Result<Char, IEnumerable<String>> result = pidgin.Parse(source);
			Assert.Equal(success, result.Success);
			if (success) {
				Assert.Equal(expected, result.Value.Join());
			}
		}

		[Theory]
		[InlineData("Hi!", false, null)]
		[InlineData("Hi!Hi!Hi!Hi!Hi!", true, "Hi!Hi!Hi!Hi!Hi!")]
		public void Sprache(String source, Boolean success, String expected) {
			IResult<IEnumerable<String>> result = sprache.TryParse(source);
			Assert.Equal(success, result.WasSuccessful);
			if (success) {
				Assert.Equal(expected, result.Value.Join());
			}
		}
	}
}
