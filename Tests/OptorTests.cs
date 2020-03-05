using System;
using Stringier.Patterns;
using static Stringier.Patterns.Pattern;
using Defender;
using Xunit;

namespace Tests {
	public class OptorTests : Trial {
		[Fact]
		public void Constructor() => Maybe("Hello");

		[Fact]
		public void Consume() =>
			Claim.That(Maybe("Hello"))
				.Consumes("Hello", "Hello World")
				.Consumes("", "Goodbye World");

		[Fact]
		public void Neglect() =>
			Claim.That(Not(Maybe("Hello")))
				.Consumes("", "Hello")
				.Consumes("World", "World");
	}
}
