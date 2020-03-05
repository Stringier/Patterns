using System;
using Stringier.Patterns;
using static Stringier.Patterns.Pattern;
using Defender;
using Xunit;

namespace Tests {
	public class CompoundPatternTests : Trial {
		[Fact]
		public void Alternate_Repeater() {
			Pattern pattern = "Hi".Or("Bye").Then(' ').Repeat(2);
			Claim.That(pattern)
				.Consumes("Hi Bye ", "Hi Bye Hi ")
				.Consumes("Hi Hi ", "Hi Hi Hi ")
				.Consumes("Hi Hi ", "Hi Hi ")
				.FailsToConsume("Hi Hi");
		}

		[Fact]
		public void Alternate_Spanner() {
			Pattern pattern = Many(SpaceSeparator.Or('\t'));
			Claim.That(pattern)
				.Consumes("  \t ", "  \t ")
				.Consumes("  \t ", "  \t hi");
		}

		[Fact]
		public void Double_Negator() {
			Pattern pattern = Not(Not("Hello"));
			Claim.That(pattern)
				.Consumes("Hello", "Hello")
				.FailsToConsume("World");
		}

		[Fact]
		public void Double_Optor() {
			Pattern pattern = Maybe(Maybe("Hello"));
			Claim.That(pattern)
				.Consumes("Hello", "Hello")
				.Consumes("", "World");
		}

		[Fact]
		public void Double_Spanner() {
			Pattern pattern = Many(Many("hi"));
			Claim.That(pattern)
				.Consumes("hi", "hi")
				.Consumes("hihi", "hihi")
				.Consumes("hihihi", "hihihi")
				.FailsToConsume("");
		}

		[Fact]
		public void Optional_Spanner() {
			Pattern pattern = Maybe(Many(' '));
			Claim.That(pattern)
				.Consumes("  ", "  Hello")
				.Consumes("", "Hello");
		}

		[Fact]
		public void Spaning_Optor() {
			Claim.That(() => Many(Maybe(' '))).Throws<PatternConstructionException>();
		}
	}
}
