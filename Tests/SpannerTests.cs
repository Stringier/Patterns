using System;
using Stringier.Patterns;
using static Stringier.Patterns.Pattern;
using Defender;
using Xunit;

namespace Tests {
	public class SpannerTests : Trial {
		[Fact]
		public void Consume() =>
			Claim.That(Many(' '))
			.Consumes(" ", " Hi!")
			.Consumes("    ", "    Hi!")
			.FailsToConsume("Hi!  ");

		[Fact]
		public void Neglect() =>
			Claim.That(Not(Many(';')))
			.Consumes("123456789", "123456789;")
			.FailsToConsume(";");
	}
}
