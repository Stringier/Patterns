using System;
using System.Text.RegularExpressions;
using Stringier.Patterns;
using static Stringier.Patterns.Pattern;
using Xunit;

namespace Tests {
	public class RegexAdapterTests {
		[Fact]
		public void Converter() {
			_ = new Regex("^hello").AsPattern();
			_ = new Regex("^hello$").AsPattern();
			Assert.Throws<PatternConstructionException>(() => new Regex("hello").AsPattern());
			Assert.Throws<PatternConstructionException>(() => new Regex("hello$").AsPattern());
		}

		[Fact]
		public void Consume() {
			Pattern pattern = new Regex("^hello").AsPattern();
			ResultAssert.Captures("hello", pattern.Consume("hello"));
			ResultAssert.Captures("hello", pattern.Consume("hello world"));
			ResultAssert.Fails(pattern.Consume("hel"));
		}
	}
}
