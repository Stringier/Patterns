using System;
using Stringier.Patterns;
using static Stringier.Patterns.Pattern;
using Defender;
using Xunit;

namespace Tests {
	public class MutableTests : Trial {
		[Fact]
		public void Mutate_And_Consume() {
			Pattern pattern = Mutable();
			pattern.Then("Hello").Then(' ').Then("world");
			Claim.That(pattern)
				.Consumes("Hello world", "Hello world");
			Not(pattern).Seal();
			Claim.That(pattern)
				.Consumes("Bacon cakes", "Bacon cakes")
				.FailsToConsume("Hello world");
		}

		[Fact]
		public void RightRecursion() {
			Pattern pattern = Mutable();
			pattern.Then("hi!").Then(EndOfSource | pattern);
			pattern.Seal();
			Claim.That(pattern)
				.Consumes("hi!", "hi!")
				.Consumes("hi!hi!", "hi!hi!")
				.Consumes("hi!hi!hi!", "hi!hi!hi!")
				.Consumes("hi!hi!hi!hi!", "hi!hi!hi!hi!")
				.FailsToConsume("");
		}

		[Fact]
		public void MutualRecursion() {
			//These grammar rules aren't great, and can easily be broken, so don't use them as a reference. They just show that mutual recursion is working.
			Pattern expression = Mutable();
			Pattern parenExpression = '(' & expression & ')';
			Pattern term = DecimalDigitNumber;
			Pattern factor = (term | parenExpression) & ('+'.Or('-')) & (term | parenExpression);
			expression.Then(factor | term | parenExpression);
			expression.Seal();
			Claim.That(expression)
				.Consumes("3+2", "3+2")
				.Consumes("3+(1+2)", "3+(1+2)")
				.Consumes("(1+2)+3", "(1+2)+3")
				.Consumes("(1+2)-(3+4)", "(1+2)-(3+4)")
				.Consumes("(1+(2+3))-4", "(1+(2+3))-4");
		}
	}
}
