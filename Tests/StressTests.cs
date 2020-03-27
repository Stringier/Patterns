using System;
using Stringier.Patterns;
using static Stringier.Patterns.Pattern;
using Defender;
using Xunit;
using FParsec.CSharp;
using Pidgin;
using Sprache;
using Benchmarks;
using static Benchmarks.StressTestBenchmarks;
using System.Text.RegularExpressions;
using PCRE;

namespace Tests {
	public class StressTests : Trial {

		[Fact]
		public void MSRegex_Good() {
			Match match = msregex.Match(goodGibberish);
			Claim.That(match.Success).Equals(true);
		}

		[Fact]
		public void PcreRegex_Good() {
			PcreMatch match = pcreregex.Match(goodGibberish);
			Claim.That(match.Success).Equals(true);
		}

		[Fact]
		public void FParsec_Good() {
			var result = fparsec.Gibberish.RunOnString(goodGibberish);
			Claim.That(result.IsSuccess).Equals(true);
		}

		[Fact]
		public void FParsec_Bad() {
			var result = fparsec.Gibberish.RunOnString(badGibberish);
			Claim.That(result.IsSuccess).Equals(false);
		}

		[Fact]
		public void Pidgin_Good() {
			var result = pidgin.Parse(goodGibberish);
			// This bizarre looking comparison is the result of Pidgin not bothering to understand end of source/stream
			Claim.That(result.Value.Length == Int32.MaxValue / ReductionFactor).Equals(true);
		}

		[Fact]
		public void Pidgin_Bad() {
			var result = pidgin.Parse(badGibberish);
			// This bizarre looking comparison is the result of Pidgin not bothering to understand end of source/stream
			Claim.That(result.Value.Length == Int32.MaxValue / ReductionFactor).Equals(false);
		}

		[Fact]
		public void Sprache_Good() {
			var result = sprache.Parse(goodGibberish);
			Claim.That(result.Length > 0).Equals(true);
		}

		[Fact]
		public void Sprache_Bad() =>
			Claim.That(() => sprache.Parse(badGibberish)).Throws<Sprache.ParseException>();

		[Fact]
		public void Stringier_Good() {
			var result = stringer.Consume(goodGibberish);
			Claim.That(result.Success).Equals(true);
		}

		[Fact]
		public void Stringer_Bad() {
			var result = stringer.Consume(badGibberish);
			Claim.That(result.Success).Equals(false);
		}
	}
}
