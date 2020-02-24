using System;
using Xunit;
using Pidgin;
using Sprache;
using static Benchmarks.NegatorBenchmarks;

namespace Benchmarks {
	public class NegatorTests {
		[Fact]
		public void Pidgin() {
			Result<Char, Pidgin.Unit> result;

			result = pidgin.Parse("World");
			Assert.True(result.Success);

			result = pidgin.Parse("Hello");
			Assert.False(result.Success);
		}

		[Fact]
		public void Sprache() {
			IResult<Object> result;

			result = sprache.TryParse("World");
			Assert.True(result.WasSuccessful);
			Assert.Null(result.Value);

			result = sprache.TryParse("Hello");
			Assert.False(result.WasSuccessful);
		}
	}
}
