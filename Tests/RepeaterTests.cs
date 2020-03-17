using System;
using System.Collections.Generic;
using Stringier;
using Stringier.Patterns;
using static Stringier.Patterns.Pattern;
using Defender;
using Xunit;
using Pidgin;
using Sprache;
using static Benchmarks.RepeaterBenchmarks;

namespace Tests {
	public class RepeaterTests : Trial {
		// We use .AsPattern() and * instead of .Repeat() here because we had to import both Stringier and Stringier.Patterns which both have .Repeat() overloads which do different things.

		[Fact]
		public void Consume() =>
			Claim.That("Hi! ".AsPattern() * 2)
			.Consumes("Hi! Hi! ", "Hi! Hi! ")
			.Consumes("Hi! Hi! ", "Hi! Hi! Hi!")
			.FailsToConsume("Hi! Bye! ");

		[Fact]
		public void Neglect() =>
			Claim.That(Not("Hi! ".AsPattern() * 2))
			.Consumes("Oh! No! ", "Oh! No! ")
			.Consumes("Oh? Hi! ", "Oh? Hi! ")
			.Consumes("Hi! Ho! ", "Hi! Ho! ")
			.FailsToConsume("H! Hi! ");

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
