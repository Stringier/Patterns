﻿using System;
using Stringier.Patterns;
using static Stringier.Patterns.Pattern;
using Xunit;
using FParsec.CSharp;
using Pidgin;
using Sprache;
using Benchmarks;
using static Benchmarks.OptorBenchmarks;

namespace Tests {
	public class OptorTests {
		[Fact]
		public void Constructor() => Maybe("Hello");

		[Fact]
		public void Consume() {
			Pattern pattern = Maybe("Hello");
			ResultAssert.Captures("Hello", pattern.Consume("Hello World"));
			ResultAssert.Captures("", pattern.Consume("Goodbye World"));
		}

		[Fact]
		public void Neglect() {
			Pattern pattern = Not(Maybe("Hello"));
			ResultAssert.Captures("", pattern.Consume("Hello"));
			ResultAssert.Captures("World", pattern.Consume("World"));
		}

		[Theory]
		[InlineData("Hello", true, "Hello")]
		[InlineData("World", true, null)]
		public void FParsec(String source, Boolean success, String expected) {
			var result = fparsec.Optor.RunOnString(source);
			Assert.Equal(success, result.IsSuccess);
			if (success) {
				Assert.Equal(expected, result.GetResult().GetValueOrDefault(null));
			}
		}

		[Theory]
		[InlineData("Hello", true, "Hello")]
		[InlineData("World", false, null)]
		public void Pidgin(String source, Boolean success, String expected) {
			Result<Char, String> result = pidgin.Parse(source);
			Assert.Equal(success, result.Success);
			if (success) {
				Assert.Equal(expected, result.Value);
			}
		}

		[Theory]
		[InlineData("Hello", true, true, "Hello")]
		[InlineData("World", true, false, null)]
		public void Sprache(String source, Boolean success, Boolean defined, String expected) {
			IResult<IOption<String>> result = sprache.TryParse(source);
			Assert.Equal(success, result.WasSuccessful);
			if (success) {
				Assert.Equal(defined, result.Value.IsDefined);
				if (defined) {
					Assert.Equal(expected, result.Value.Get());
				}
			}
		}
	}
}
