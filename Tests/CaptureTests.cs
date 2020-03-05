using System;
using Stringier.Patterns;
using static Stringier.Patterns.Pattern;
using Defender;
using Xunit;

namespace Tests {
	public class CaptureTests : Trial {
		[Fact]
		public void Constructor() => 'a'.Or('b').Or('c').Capture(out Capture capture);

		[Fact]
		public void Consume() {
			Capture capture;
			Pattern pattern = 'a'.Or('b').Or('c').Capture(out capture);
			Claim.That(pattern).Consumes("a", "a");
			Claim.That(capture).Captures("a");

			pattern = 'a'.Or('b').Or('c').Capture(out capture).Then('!');
			Claim.That(pattern)
				.FailsToConsume("a")
				.Consumes("a!", "a!");
			Claim.That(capture).Captures("a");

			pattern = 'a'.Or('b').Or('c').Capture(out capture).Then(',').Then(capture);
			Claim.That(pattern)
				.Consumes("b,b", "b,b")
				.FailsToConsume("b,a");
		}
	}
}
