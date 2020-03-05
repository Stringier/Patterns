using System;
using Stringier.Patterns;
using static Stringier.Patterns.Pattern;
using Defender;
using Xunit;

namespace Tests {
	public class RangerTests : Trial {
		[Fact]
		public void Simple_Consume() =>
			Claim.That(Range("Hello", ';'))
			.Consumes("Hello;", "Hello;")
			.Consumes("Hello World;", "Hello World;");

		[Fact]
		public void Simple_Neglect() =>
			Claim.That(() => Not(Range("Hello", ";"))).Throws<PatternConstructionException>();

		[Fact]
		public void Escaped_Consume() =>
			Claim.That(Range("\"", "\"", "\\\""))
			.Consumes("\"\"", "\"\"")
			.Consumes("\"H\"", "\"H\"i")
			.Consumes("\"Hello\"", "\"Hello\"")
			.Consumes("\"Hello\\\"Goodbye\"", "\"Hello\\\"Goodbye\"");

		[Fact]
		public void Nested_Consume() =>
			Claim.That(NestedRange("if", "end if"))
			.Consumes("if\nif\nend if\nbacon\nend if", "if\nif\nend if\nbacon\nend if\nfoobar")
			.FailsToConsume("if\nif\nend if\nbacon\nfoobar");
	}
}
