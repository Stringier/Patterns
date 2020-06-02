using System;
using System.Text;
using Stringier;
using Stringier.Patterns;
using static Stringier.Patterns.Pattern;
using Xunit;
using FParsec.CSharp;
using Pidgin;
using Sprache;
using Benchmarks;
using static Benchmarks.LiteralBenchmarks;

namespace Tests {
	public class LiteralTests {
		[Fact]
		public void Constructor() {
			Pattern _ = 'h';
			_ = "hello";
		}

		[Fact]
		public void Consume_String_FromString() {
			ResultAssert.Captures("Hello", "Hello".Consume("Hello World"));
			ResultAssert.Fails("Hello".Consume("Hell"));
			ResultAssert.Fails("Hello".Consume("Bacon"));
		}

		[Fact]
		public void Consume_String_FromSource() {
			Pattern hello = "Hello";
			Pattern space = ' ';
			Pattern world = "World";
			Source source = new Source("Hello World");
			ResultAssert.Captures("Hello", hello.Consume(ref source));
			ResultAssert.Captures(" ", space.Consume(ref source));
			ResultAssert.Captures("World", world.Consume(ref source));
			source = new Source("Hello World");
			ResultAssert.Captures("Hello World", (hello & space & world).Consume(ref source));
		}

		[Fact]
		public void Consume_Rune_FromSource() {
			Source source = new Source("hello");
			ResultAssert.Captures("h", new Rune('h').Consume(ref source));
			ResultAssert.Captures("ello", "ello".Consume(ref source));
			source = new Source("𝄞abc");
			ResultAssert.Captures("𝄞", new Rune(0x1D11E).Consume(ref source));
			ResultAssert.Captures("abc", "abc".Consume(ref source));
		}

		[Fact]
		public void Consume_Insensitive() {
			Pattern pattern = "Hello".With(Case.Insensitive).Then(' '.With(Case.Insensitive)).Then("World".With(Case.Insensitive));
			ResultAssert.Captures("HELLO WORLD", pattern.Consume("HELLO WORLD"));
			ResultAssert.Captures("hello world", pattern.Consume("hello world"));
		}

		[Fact]
		public void Neglect() {
			Pattern pattern = Not("Hello");
			ResultAssert.Captures("Oh no", pattern.Consume("Oh no!"));
			ResultAssert.Captures("Oh no", pattern.Consume("Oh no?"));
			ResultAssert.Fails(pattern.Consume("Hello"));
			ResultAssert.Fails(pattern.Consume("Hello!"));
			ResultAssert.Fails(pattern.Consume("Hello."));
		}

		[Theory]
		[InlineData("Hello", true, "Hello")]
		[InlineData("World", false, null)]
		[InlineData("F", false, null)]
		[InlineData("Failure", false, null)]
		public void FParsec(String source, Boolean success, String expected) {
			var result = fparsec.Literal.RunOnString(source);
			Assert.Equal(success, result.IsSuccess);
			if (success) {
				Assert.Equal(expected, result.GetResult());
			}
		}

		[Theory]
		[InlineData("Hello", true, "Hello")]
		[InlineData("World", false, null)]
		[InlineData("F", false, null)]
		[InlineData("Failure", false, null)]
		public void Pidgin(String source, Boolean success, String expected) {
			Result<Char, String> result = pidgin.Parse(source);
			Assert.Equal(success, result.Success);
			if (success) {
				Assert.Equal(expected, result.Value);
			}
		}

		[Theory]
		[InlineData("Hello", true, "Hello")]
		[InlineData("World", false, null)]
		[InlineData("F", false, null)]
		[InlineData("Failure", false, null)]
		public void Sprache(String source, Boolean success, String expected) {
			IResult<String> result = sprache.TryParse(source);
			Assert.Equal(success, result.WasSuccessful);
			if (success) {
				Assert.Equal(expected, result.Value);
			}
		}
	}
}
