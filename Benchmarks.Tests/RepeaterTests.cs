using System;
using System.Collections.Generic;
using Xunit;
using Pidgin;
using Stringier;
using Sprache;
using static Benchmarks.RepeaterBenchmarks;

namespace Benchmarks {
	public class RepeaterTests {
		[Fact]
		public void Pidgin() {
			Result<Char, IEnumerable<String>> result;

			result = pidgin.Parse("Hi!Hi!Hi!Hi!Hi!");
			Assert.True(result.Success);
			Assert.Equal("Hi!Hi!Hi!Hi!Hi!", result.Value.Join());

			result = pidgin.Parse("Hi!");
			Assert.False(result.Success);
		}

		[Fact]
		public void Sprache() {
			IResult<IEnumerable<String>> result;

			result = sprache.TryParse("Hi!Hi!Hi!Hi!Hi!");
			Assert.True(result.WasSuccessful);
			Assert.Equal("Hi!Hi!Hi!Hi!Hi!", result.Value.Join());

			result = sprache.TryParse("Hi!");
			Assert.False(result.WasSuccessful);
		}
	}
}
