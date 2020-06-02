using System;
using Stringier.Patterns;
using static Stringier.Patterns.Pattern;
using Defender;
using Xunit;

namespace Tests {
	public class CompoundPatternTests {
		[Fact]
		public void Alternate_Repeater() {
			Pattern pattern = "Hi".Or("Bye").Then(' ').Repeat(2);
			ResultAssert.Captures("Hi Bye ", pattern.Consume("Hi Bye Hi "));
			ResultAssert.Captures("Hi Hi ", pattern.Consume("Hi Hi Hi "));
			ResultAssert.Captures("Hi Hi ", pattern.Consume("Hi Hi "));
			ResultAssert.Fails(pattern.Consume("Hi Hi"));
		}

		[Fact]
		public void Alternate_Spanner() {
			Pattern pattern = Many(SpaceSeparator.Or('\t'));
			ResultAssert.Captures("  \t ", pattern.Consume("  \t "));
			ResultAssert.Captures("  \t ", pattern.Consume("  \t hi"));
		}

		[Fact]
		public void Double_Negator() {
			Pattern pattern = Not(Not("Hello"));
			ResultAssert.Captures("Hello", pattern.Consume("Hello"));
			ResultAssert.Fails(pattern.Consume("World"));
		}

		[Fact]
		public void Double_Optor() {
			Pattern pattern = Maybe(Maybe("Hello"));
			ResultAssert.Captures("Hello", pattern.Consume("Hello"));
			ResultAssert.Captures("", pattern.Consume("World"));
		}

		[Fact]
		public void Double_Spanner() {
			Pattern pattern = Many(Many("hi"));
			ResultAssert.Captures("hi", pattern.Consume("hi"));
			ResultAssert.Captures("hihi", pattern.Consume("hihi"));
			ResultAssert.Captures("hihihi", pattern.Consume("hihihi"));
			ResultAssert.Fails(pattern.Consume(""));
		}

		[Fact]
		public void Optional_Spanner() {
			Pattern pattern = Maybe(Many(' '));
			ResultAssert.Captures("  ", pattern.Consume("  Hello"));
			ResultAssert.Captures("", pattern.Consume("Hello"));
		}

		[Fact]
		public void Spaning_Optor() => Assert.Throws<PatternConstructionException>(() => Many(Maybe(' ')));
	}
}
