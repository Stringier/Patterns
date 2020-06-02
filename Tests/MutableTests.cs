using System;
using Stringier.Patterns;
using static Stringier.Patterns.Pattern;
using Xunit;

namespace Tests {
	public class MutableTests {
		[Fact]
		public void Mutate_And_Consume() {
			Pattern pattern = Mutable();
			pattern.Then("Hello").Then(' ').Then("world");
			ResultAssert.Captures("Hello world", pattern.Consume("Hello world"));
			Not(pattern).Seal();
			ResultAssert.Captures("Bacon cakes", pattern.Consume("Bacon cakes"));
			ResultAssert.Fails(pattern.Consume("Hello world"));
		}

		[Fact]
		public void RightRecursion() {
			Pattern pattern = Mutable();
			pattern.Then("hi!").Then(EndOfSource | pattern);
			pattern.Seal();
			ResultAssert.Captures("hi!", pattern.Consume("hi!"));
			ResultAssert.Captures("hi!hi!", pattern.Consume("hi!hi!"));
			ResultAssert.Captures("hi!hi!hi!", pattern.Consume("hi!hi!hi!"));
			ResultAssert.Captures("hi!hi!hi!hi!", pattern.Consume("hi!hi!hi!hi!"));
			ResultAssert.Fails(pattern.Consume(""));
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
			ResultAssert.Captures("3+2", expression.Consume("3+2"));
			ResultAssert.Captures("3+(1+2)", expression.Consume("3+(1+2)"));
			ResultAssert.Captures("(1+2)+3", expression.Consume("(1+2)+3"));
			ResultAssert.Captures("(1+2)-(3+4)", expression.Consume("(1+2)-(3+4)"));
			ResultAssert.Captures("(1+(2+3))-4", expression.Consume("(1+(2+3))-4"));
		}
	}
}
