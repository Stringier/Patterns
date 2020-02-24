using System;
using System.Collections.Generic;
using Xunit;
using Pidgin;
using Sprache;
using Stringier;
using static Benchmarks.SpannerBenchmarks;

namespace Benchmarks {
	public class SpannerTests {
		[Fact]
		public void Pidgin() {
			Result<Char, String> result;

			result = pidgin.Parse("Hi!");
			Assert.True(result.Success);
			Assert.Equal("Hi!", result.Value);

			result = pidgin.Parse("Hi!Hi!");
			Assert.True(result.Success);
			Assert.Equal("Hi!Hi!", result.Value);

			result = pidgin.Parse("Hi!Hi!Hi!");
			Assert.True(result.Success);
			Assert.Equal("Hi!Hi!Hi!", result.Value);

			result = pidgin.Parse("Okay?");
			Assert.False(result.Success);
		}

		[Fact]
		public void Sprache() {
			IResult<IEnumerable<String>> result;

			result = sprache.TryParse("Hi!");
			Assert.True(result.WasSuccessful);
			Assert.Equal("Hi!", result.Value.Join());

			result = sprache.TryParse("Hi!Hi!");
			Assert.True(result.WasSuccessful);
			Assert.Equal("Hi!Hi!", result.Value.Join());

			result = sprache.TryParse("Hi!Hi!Hi!");
			Assert.True(result.WasSuccessful);
			Assert.Equal("Hi!Hi!Hi!", result.Value.Join());

			result = sprache.TryParse("Okay?");
			Assert.False(result.WasSuccessful);
		}
	}
}
