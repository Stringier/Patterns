using System;
using Xunit;
using Pidgin;
using Sprache;
using static Benchmarks.ConcatenatorBenchmarks;

namespace Benchmarks {
	public class ConcatenatorTests {
		[Fact]
		public void Pidgin() {
			Result<Char, String> result;

			result = pidgin.Parse("Hello World");
			Assert.True(result.Success);
			Assert.Equal("Hello World", result.Value);

			result = pidgin.Parse("Hello");
			Assert.False(result.Success);
		}

		[Fact]
		public void Sprache() {
			IResult<String> result;

			result = sprache.TryParse("Hello World");
			Assert.True(result.WasSuccessful);
			Assert.Equal("Hello World", result.Value);

			result = sprache.TryParse("Hello");
			Assert.False(result.WasSuccessful);
		}
	}
}
