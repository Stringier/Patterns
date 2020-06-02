using System;
using System.Collections.Generic;
using Stringier.Patterns;
using static Stringier.Patterns.Pattern;
using Xunit;
using FParsec.CSharp;
using Pidgin;
using Sprache;
using Stringier;
using Benchmarks;
using static Benchmarks.SpannerBenchmarks;

namespace Tests {
	public class SpannerTests {
		[Fact]
		public void Consume() {
			Pattern pattern = Many(' ');
			ResultAssert.Captures(" ", pattern.Consume(" Hi!"));
			ResultAssert.Captures("    ", pattern.Consume("    Hi!"));
			ResultAssert.Fails(pattern.Consume("Hi!  "));
		}

		[Fact]
		public void Neglect() {
			Pattern pattern = Not(Many(';'));
			ResultAssert.Captures("123456789", pattern.Consume("123456789;"));
			ResultAssert.Fails(pattern.Consume(";"));
		}

		[Theory]
		[InlineData("Hi!", true, "Hi!")]
		[InlineData("Hi!Hi!", true, "Hi!Hi!")]
		[InlineData("Hi!Hi!Hi!", true, "Hi!Hi!Hi!")]
		[InlineData("Okay?", false, null)]
		public void FParsec(String source, Boolean success, String expected) {
			var result = fparsec.Spanner.RunOnString(source);
			Assert.Equal(success, result.IsSuccess);
			if (success) {
				Assert.Equal(expected, result.GetResult());
			}
		}

		[Theory]
		[InlineData("Hi!", true, "Hi!")]
		[InlineData("Hi!Hi!", true, "Hi!Hi!")]
		[InlineData("Hi!Hi!Hi!", true, "Hi!Hi!Hi!")]
		[InlineData("Okay?", false, null)]
		public void Pidgin(String source, Boolean success, String expected) {
			Result<Char, String> result = pidgin.Parse(source);
			Assert.Equal(success, result.Success);
			if (success) {
				Assert.Equal(expected, result.Value);
			}
		}

		[Theory]
		[InlineData("Hi!", true, "Hi!")]
		[InlineData("Hi!Hi!", true, "Hi!Hi!")]
		[InlineData("Hi!Hi!Hi!", true, "Hi!Hi!Hi!")]
		[InlineData("Okay?", false, null)]
		public void Sprache(String source, Boolean success, String expected) {
			IResult<IEnumerable<String>> result = sprache.TryParse(source);
			Assert.Equal(success, result.WasSuccessful);
			if (success) {
				Assert.Equal(expected, result.Value.Join());
			}
		}
	}
}
