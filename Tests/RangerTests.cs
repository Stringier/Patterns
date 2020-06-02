using System;
using Stringier.Patterns;
using static Stringier.Patterns.Pattern;
using Xunit;

namespace Tests {
	public class RangerTests {
		[Fact]
		public void Simple_Consume() {
			Pattern pattern = Range("Hello", ';');
			ResultAssert.Captures("Hello;", pattern.Consume("Hello;"));
			ResultAssert.Captures("Hello World;", pattern.Consume("Hello World;"));
		}

		[Fact]
		public void Simple_Neglect() => Assert.Throws<PatternConstructionException>(() => Not(Range("Hello", ";")));

		[Fact]
		public void Escaped_Consume() {
			Pattern pattern = Range("\"", "\"", "\\\"");
			ResultAssert.Captures("\"\"", pattern.Consume("\"\""));
			ResultAssert.Captures("\"H\"", pattern.Consume("\"H\"i"));
			ResultAssert.Captures("\"Hello\"", pattern.Consume("\"Hello\""));
			ResultAssert.Captures("\"Hello\\\"Goodbye\"", pattern.Consume("\"Hello\\\"Goodbye\""));
		}

		[Fact]
		public void Nested_Consume() {
			Pattern pattern = NestedRange("if", "end if");
			ResultAssert.Captures("if\nif\nend if\nbacon\nend if", pattern.Consume("if\nif\nend if\nbacon\nend if\nfoobar"));
			ResultAssert.Fails(pattern.Consume("if\nif\nend if\nbacon\nfoobar"));
		}
	}
}
