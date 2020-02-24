using System;
using Xunit;
using Pidgin;
using Sprache;
using static Benchmarks.LiteralBenchmarks;

namespace Benchmarks {
	public class LiteralTests {
		[Fact]
		public void Pidgin() {
			Result<Char, String> result;

			result = pidgin.Parse("Hello");
			Assert.True(result.Success);
			Assert.Equal("Hello", result.Value);

			result = pidgin.Parse("World");
			Assert.False(result.Success);

			result = pidgin.Parse("H");
			Assert.False(result.Success);

			result = pidgin.Parse("Failure");
			Assert.False(result.Success);
		}

		[Fact]
		public void Sprache() {
			IResult<String> result;

			result = sprache.TryParse("Hello");
			Assert.True(result.WasSuccessful);
			Assert.Equal("Hello", result.Value);

			result = sprache.TryParse("World");
			Assert.False(result.WasSuccessful);

			result = sprache.TryParse("H");
			Assert.False(result.WasSuccessful);

			result = sprache.TryParse("Failure");
			Assert.False(result.WasSuccessful);
		}
	}
}
