using System;
using Stringier;
using Stringier.Patterns;
using static Stringier.Patterns.Pattern;
using Defender;
using Xunit;

namespace Tests {
	public class RealWorldTests : Trial {
		[Fact]
		public void Backtracking() {
			Pattern end1 = "end".Then(Many(' ')).Then("first");
			Pattern end2 = "end".Then(Many(' ')).Then("second");
			Source source = new Source("end second");
			//f backtracking doesn't occur, parsing end2 will fail, because "end" and the space will have been consumed
			Claim.That(end1).FailsToConsume(ref source);
			Claim.That(end2).Consumes("end second", ref source);
		}

		[Fact]
		public void Comment() =>
			Claim.That(LineComment("--"))
			.Consumes("--This is a comment", "--This is a comment")
			.Consumes("--This is also a comment", "--This is also a comment\nExample_Function_Call();");

		[Fact]
		public void Identifier() {
			Pattern pattern = Check(
				(c) => c.IsLetter(), true,
				(c) => c.IsLetter() || c == '_', true,
				(c) => c.IsLetter() || c == '_', false);
			Claim.That(pattern)
				.Consumes("hello", "hello")
				.Consumes("example_name", "example_name")
				.FailsToConsume("_fail");
		}

		[Fact]
		public void IPv4Address() {
			//This doesn't really work, but it's close enough for testing
			Pattern digit = Check(
				(c) => '0' <= c && c <= '2', false,
				(c) => '0' <= c && c <= '9', false,
				(c) => '0' <= c && c <= '9', true);
			Pattern address = digit & '.' & digit & '.' & digit & '.' & digit;
			Claim.That(digit)
				.Consumes("1", "1")
				.Consumes("11", "11")
				.Consumes("111", "111");
			Claim.That(address)
				.Consumes("192.168.1.1", "192.168.1.1");
		}

		[Fact]
		public void NamedStatement() {
			Pattern identifier = Check(
				(c) => c.IsLetter() || c == '_',
				(c) => c.IsLetterOrDigit() || c == '_',
				(c) => c.IsLetterOrDigit());
			Claim.That(identifier).Consumes("Name", "Name");
			Pattern statement = "statement" & Many(Separator) & identifier.Capture(out Capture capture);
			Claim.That(statement).Consumes("statement Name", "statement Name");
			Claim.That(capture).Captures("Name");
		}

		[Fact]
		public void PhoneNumber() {
			Pattern pattern = Number * 3 & '-' & Number * 3 & '-' & Number * 4;
			Claim.That(pattern).Consumes("555-555-5555", "555-555-5555");
		}

		[Fact]
		public void ReadUntilEndOfLine() =>
			Claim.That(Many(Not(';' | LineTerminator)))
			.Consumes("hello", "hello")
			.Consumes("hello", "hello\nworld");

		[Fact]
		public void StringLiteral() =>
			Claim.That(Pattern.StringLiteral("\"", "\\\""))
			.Consumes("\"Hello World\"", "\"Hello World\"")
			.Consumes("\"Hello World\"", "\"Hello World\" Bacon")
			.Consumes("\"Hello\\\"World\"", "\"Hello\\\"World\"")
			.Consumes("\"Hello\\\"World\"", "\"Hello\\\"World\" Bacon");

		[Fact]
		public void WebAddress() {
			Pattern protocol = "http" & Maybe('s') & "://";
			Pattern host = Many(Letter | Number | '-') & '.' & (Letter * 3 & EndOfSource | Many(Letter | Number | '-') & '.' & Letter * 3);
			Pattern location = Many('/' & Many(Letter | Number | '-' | '_'));
			Pattern address = Maybe(protocol) & host & Maybe(location);
			Claim.That(protocol)
				.Consumes("http://", "http://")
				.Consumes("http://", "http://www.google.com")
				.Consumes("https://", "https://")
				.Consumes("https://", "https://www.google.com");
			Claim.That(host)
				.Consumes("google.com", "google.com")
				.Consumes("www.google.com", "www.google.com");
			Claim.That(location)
				.Consumes("/about", "/about");
			Claim.That(address)
				.Consumes("http://www.google.com", "http://www.google.com")
				.Consumes("https://www.google.com", "https://www.google.com");
		}
	}
}
