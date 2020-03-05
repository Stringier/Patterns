using System;
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
		public void Consume_String() {
			Claim.That("Hello")
				.Consumes("Hello", "Hello World")
				.FailsToConsume("Hell")
				.FailsToConsume("Bacon");
		}

		[Fact]
		public void Consume_Source() {
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
		public void Consume_CaseInsensitive() {
			Pattern pattern = "Hello".With(Compare.CaseInsensitive).Then(' '.With(Compare.CaseInsensitive)).Then("World".With(Compare.CaseInsensitive));
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
