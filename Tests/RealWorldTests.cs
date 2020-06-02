using System;
using Stringier;
using Stringier.Patterns;
using static Stringier.Patterns.Pattern;
using Xunit;

namespace Tests {
	public class RealWorldTests {
		[Fact]
		public void Backtracking() {
			Pattern end1 = "end".Then(Many(' ')).Then("first");
			Pattern end2 = "end".Then(Many(' ')).Then("second");
			Source source = new Source("end second");
			//f backtracking doesn't occur, parsing end2 will fail, because "end" and the space will have been consumed
			ResultAssert.Fails(end1.Consume(ref source));
			ResultAssert.Captures("end second", end2.Consume(ref source));
		}

		[Fact]
		public void Comment() {
			Pattern pattern = LineComment("--");
			ResultAssert.Captures("--This is a comment", pattern.Consume("--This is a comment"));
			ResultAssert.Captures("--This is also a comment", pattern.Consume("--This is also a comment\nExample_Function_Call();"));
		}

		[Fact]
		public void Identifier() {
			Pattern pattern = Check(
				(c) => c.IsLetter(), true,
				(c) => c.IsLetter() || c == '_', true,
				(c) => c.IsLetter() || c == '_', false);
			ResultAssert.Captures("hello", pattern.Consume("hello"));
			ResultAssert.Captures("example_name", pattern.Consume("example_name"));
			ResultAssert.Fails(pattern.Consume("_fail"));
		}

		[Fact]
		public void IPv4Address() {
			//This doesn't really work, but it's close enough for testing
			Pattern digit = Check(
				(c) => '0' <= c && c <= '2', false,
				(c) => '0' <= c && c <= '9', false,
				(c) => '0' <= c && c <= '9', true);
			ResultAssert.Captures("1", digit.Consume("1"));
			ResultAssert.Captures("11", digit.Consume("11"));
			ResultAssert.Captures("111", digit.Consume("111"));
			Pattern address = digit & '.' & digit & '.' & digit & '.' & digit;
			ResultAssert.Captures("192.168.1.1", address.Consume("192.168.1.1"));
		}

		[Fact]
		public void NamedStatement() {
			Pattern identifier = Check(
				(c) => c.IsLetter() || c == '_',
				(c) => c.IsLetterOrDigit() || c == '_',
				(c) => c.IsLetterOrDigit());
			ResultAssert.Captures("Name", identifier.Consume("Name"));
			Pattern statement = "statement" & Many(Separator) & identifier.Capture(out Capture capture);
			ResultAssert.Captures("statement Name", statement.Consume("statement Name"));
			CaptureAssert.Captures("Name", capture);
		}

		[Fact]
		public void PhoneNumber() {
			Pattern pattern = Number * 3 & '-' & Number * 3 & '-' & Number * 4;
			ResultAssert.Captures("555-555-5555", pattern.Consume("555-555-5555"));
		}

		[Fact]
		public void ReadUntilEndOfLine() {
			Pattern pattern = Many(Not(';' | LineTerminator));
			ResultAssert.Captures("hello", pattern.Consume("hello"));
			ResultAssert.Captures("hello", pattern.Consume("hello\nworld"));
		}

		[Fact]
		public void StringLiteral() {
			Pattern pattern = Pattern.StringLiteral("\"", "\\\"");
			ResultAssert.Captures("\"Hello World\"", pattern.Consume("\"Hello World\""));
			ResultAssert.Captures("\"Hello World\"", pattern.Consume("\"Hello World\" Bacon"));
			ResultAssert.Captures("\"Hello\\\"World\"", pattern.Consume("\"Hello\\\"World\""));
			ResultAssert.Captures("\"Hello\\\"World\"", pattern.Consume("\"Hello\\\"World\" Bacon"));
		}

		[Fact]
		public void WebAddress() {
			Pattern protocol = "http" & Maybe('s') & "://";
			Pattern host = Many(Letter | Number | '-') & '.' & (Letter * 3 & EndOfSource | Many(Letter | Number | '-') & '.' & Letter * 3);
			Pattern location = Many('/' & Many(Letter | Number | '-' | '_'));
			Pattern address = Maybe(protocol) & host & Maybe(location);
			ResultAssert.Captures("http://", protocol.Consume("http://"));
			ResultAssert.Captures("http://", protocol.Consume("http://www.google.com"));
			ResultAssert.Captures("https://", protocol.Consume("https://"));
			ResultAssert.Captures("https://", protocol.Consume("https://www.google.com"));
			ResultAssert.Captures("google.com", host.Consume("google.com"));
			ResultAssert.Captures("www.google.com", host.Consume("www.google.com"));
			ResultAssert.Captures("/about", location.Consume("/about"));
			ResultAssert.Captures("http://www.google.com", address.Consume("http://www.google.com"));
			ResultAssert.Captures("https://www.google.com", address.Consume("https://www.google.com"));
		}
	}
}
