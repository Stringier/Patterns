using System;
using System.Text.RegularExpressions;
using Stringier.Patterns;
using static Stringier.Patterns.Pattern;
using Defender;
using Xunit;

namespace Tests {
	public class RegexAdapterTests : Trial {
		[Fact]
		public void Converter() {
			_ = new Regex("^hello").AsPattern();
			_ = new Regex("^hello$").AsPattern();
			Claim.That(() => new Regex("hello").AsPattern())
				.Throws<PatternConstructionException>();
			Claim.That(() => new Regex("hello$").AsPattern())
				.Throws<PatternConstructionException>();
		}

		[Fact]
		public void Consume() =>
			Claim.That(new Regex("^hello").AsPattern())
			.Consumes("hello", "hello")
			.Consumes("hello", "hello world")
			.FailsToConsume("hel");
	}
}
