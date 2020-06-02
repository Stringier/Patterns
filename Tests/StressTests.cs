using System;
using Stringier.Patterns;
using static Stringier.Patterns.Pattern;
using Xunit;
using FParsec.CSharp;
using Pidgin;
using Sprache;
using Benchmarks;
using static Benchmarks.StressTestBenchmarks;
using System.Text.RegularExpressions;
using PCRE;

namespace Tests {
	public class StressTests {
		[Fact]
		public void MSRegex_Good() {
			Match match = msregex.Match(goodGibberish);
			Assert.True(match.Success);
		}

		[Fact]
		public void PcreRegex_Good() {
			PcreMatch match = pcreregex.Match(goodGibberish);
			Assert.True(match.Success);
		}

		[Fact]
		public void FParsec_Good() {
			var result = fparsec.Gibberish.RunOnString(goodGibberish);
			Assert.True(result.IsSuccess);
		}

		[Fact]
		public void FParsec_Bad() {
			var result = fparsec.Gibberish.RunOnString(badGibberish);
			Assert.False(result.IsSuccess);
		}

		[Fact]
		public void Pidgin_Good() {
			var result = pidgin.Parse(goodGibberish);
			// This bizarre looking comparison is the result of Pidgin not bothering to understand end of source/stream
			Assert.True(result.Value.Length == Int32.MaxValue / ReductionFactor);
		}

		[Fact]
		public void Pidgin_Bad() {
			var result = pidgin.Parse(badGibberish);
			// This bizarre looking comparison is the result of Pidgin not bothering to understand end of source/stream
			Assert.False(result.Value.Length == Int32.MaxValue / ReductionFactor);
		}

		[Fact]
		public void Sprache_Good() {
			var result = sprache.Parse(goodGibberish);
			Assert.True(result.Length > 0);
		}

		[Fact]
		public void Sprache_Bad() => Assert.Throws<Sprache.ParseException>(() => sprache.Parse(badGibberish));

		[Fact]
		public void Stringier_Good() {
			var result = stringer.Consume(goodGibberish);
			Assert.True(result.Success);
		}

		[Fact]
		public void Stringer_Bad() {
			var result = stringer.Consume(badGibberish);
			Assert.False(result.Success);
		}
	}
}
