using System;
using Xunit;
using Pidgin;
using Sprache;
using static Benchmarks.AlternatorBenchmarks;

namespace Benchmarks {
	public class AlternatorTests {
		[Fact]
		public void Pidgin() {
			Result<Char, String> result;

			result = pidgin.Parse("Hello");
			Assert.True(result.Success);
			Assert.Equal("Hello", result.Value);

			result = pidgin.Parse("Goodbye");
			Assert.True(result.Success);
			Assert.Equal("Goodbye", result.Value);

			result = pidgin.Parse("Bacon");
			Assert.False(result.Success);
		}

		[Fact]
		public void Sprache() {
			IResult<String> result;

			result = sprache.TryParse("Hello");
			Assert.True(result.WasSuccessful);
			Assert.Equal("Hello", result.Value);

			result = sprache.TryParse("Goodbye");
			Assert.True(result.WasSuccessful);
			Assert.Equal("Goodbye", result.Value);

			result = sprache.TryParse("Bacon");
			Assert.False(result.WasSuccessful);
		}
	}
}
