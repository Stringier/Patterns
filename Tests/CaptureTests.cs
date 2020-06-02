using System;
using Stringier.Patterns;
using static Stringier.Patterns.Pattern;
using Xunit;

namespace Tests {
	public class CaptureTests {
		[Fact]
		public void Constructor() => 'a'.Or('b').Or('c').Capture(out Capture capture);

		[Fact]
		public void Consume() {
			Capture capture;
			Pattern pattern = 'a'.Or('b').Or('c').Capture(out capture);
			ResultAssert.Captures("a", pattern.Consume("a"));
			CaptureAssert.Captures("a", capture);

			pattern = 'a'.Or('b').Or('c').Capture(out capture).Then('!');
			ResultAssert.Fails(pattern.Consume("a")); 
			ResultAssert.Captures("a!", pattern.Consume("a!"));
			CaptureAssert.Captures("a", capture);

			pattern = 'a'.Or('b').Or('c').Capture(out capture).Then(',').Then(capture);
			ResultAssert.Captures("b,b", pattern.Consume("b,b"));
			ResultAssert.Fails(pattern.Consume("b,a"));
		}
	}
}
