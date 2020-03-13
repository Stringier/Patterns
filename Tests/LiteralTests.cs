using System;
using System.Text;
using Stringier;
using Stringier.Patterns;
using static Stringier.Patterns.Pattern;
using Defender;
using Xunit;

namespace Tests {
	public class LiteralTests : Trial {
		[Fact]
		public void Constructor() {
			Pattern _ = 'h';
			_ = "hello";
		}

		[Fact]
		public void Consume_String_FromString() {
			Claim.That("Hello")
				.Consumes("Hello", "Hello World")
				.FailsToConsume("Hell")
				.FailsToConsume("Bacon");
		}

		[Fact]
		public void Consume_String_FromSource() {
			Pattern hello = "Hello";
			Pattern space = ' ';
			Pattern world = "World";
			Source source = new Source("Hello World");
			Claim.That(hello).Consumes("Hello", ref source);
			Claim.That(space).Consumes(" ", ref source);
			Claim.That(world).Consumes("World", ref source);
			source = new Source("Hello World");
			Claim.That(hello.Then(space).Then(world)).Consumes("Hello World", ref source);
		}

		[Fact]
		public void Consume_Rune_FromSource() {
			Source source = new Source("hello");
			Pattern pattern = new Rune('h');
			Claim.That(pattern).Consumes("h", ref source);
			Claim.That("ello").Consumes("ello", ref source);
			source = new Source("𝄞abc");
			pattern = new Rune(0x1D11E);
			Claim.That(pattern).Consumes("𝄞", ref source);
			Claim.That("abc").Consumes("abc", ref source);
		}

		[Fact]
		public void Consume_Insensitive() {
			Pattern pattern = "Hello".With(Case.Insensitive).Then(' '.With(Case.Insensitive)).Then("World".With(Case.Insensitive));
			Claim.That(pattern)
				.Consumes("HELLO WORLD", "HELLO WORLD")
				.Consumes("hello world", "hello world");
		}

		[Fact]
		public void Neglect() {
			Claim.That(Not("Hello"))
				.Consumes("Oh no", "Oh no!")
				.Consumes("Oh no", "Oh no?")
				.FailsToConsume("Hello")
				.FailsToConsume("Hello!")
				.FailsToConsume("Hello.");
		}
	}
}
