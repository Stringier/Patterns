using System;
using Stringier.Patterns;
using static Stringier.Patterns.Pattern;
using Defender;
using Xunit;

namespace Tests {
	public class RepeaterTests : Trial {
		[Fact]
		public void Consume() =>
			Claim.That("Hi! ".Repeat(2))
			.Consumes("Hi! Hi! ", "Hi! Hi! ")
			.Consumes("Hi! Hi! ", "Hi! Hi! Hi!")
			.FailsToConsume("Hi! Bye! ");

		[Fact]
		public void Neglect() =>
			Claim.That(Not("Hi! ".Repeat(2)))
			.Consumes("Oh! No! ", "Oh! No! ")
			.Consumes("Oh? Hi! ", "Oh? Hi! ")
			.Consumes("Hi! Ho! ", "Hi! Ho! ")
			.FailsToConsume("H! Hi! ");
	}
}
