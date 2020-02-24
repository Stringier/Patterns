using System;
using Xunit;
using Pidgin;
using Sprache;
using static Benchmarks.OptorBenchmarks;

namespace Benchmarks {
	public class OptorTests {
		[Fact]
		public void Pidgin() {
			Result<Char, String> result;

			result = pidgin.Parse("Hello");
			Assert.True(result.Success);
			Assert.Equal("Hello", result.Value);

			result = pidgin.Parse("World");
			Assert.False(result.Success);
		}

		[Fact]
		public void Sprache() {
			IResult<IOption<String>> result;

			result = sprache.TryParse("Hello");
			Assert.True(result.WasSuccessful);
			Assert.True(result.Value.IsDefined);
			Assert.Equal("Hello", result.Value.Get());

			result = sprache.TryParse("World");
			Assert.True(result.WasSuccessful);
			Assert.False(result.Value.IsDefined);
		}
	}
}
