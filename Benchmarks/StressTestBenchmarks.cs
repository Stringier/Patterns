using System;
using System.Text.RegularExpressions;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using PCRE;
using FParsec.CSharp;
using Pidgin;
using Sprache;
using Stringier;
using Stringier.Patterns;
using static Stringier.Patterns.Pattern;

namespace Benchmarks {
	[SimpleJob(RuntimeMoniker.NetCoreApp31)]
	[MemoryDiagnoser]
	public class StressTestBenchmarks {
		public const Int32 ReductionFactor = 2048;

		public static String goodGibberish = Gibberish.Generate(ReductionFactor);

		public static String badGibberish = Gibberish.Generate(ReductionFactor, bad: true);

		public static readonly Regex msregex = new Regex(@"(?:[a-z]+| +)+", RegexOptions.Singleline);

		public static readonly PcreRegex pcreregex = new PcreRegex(@"(?:[a-z]+| +)+", PcreOptions.Singleline);

		public static readonly Parser<Char, String> pidgin = Parser.ManyString(Parser.Letter.Or(Parser.Char(' ')));

		public static readonly Sprache.Parser<String> sprache = Parse.Letter.Or(Parse.Char(' ')).Many().End().Text();

		public static readonly Pattern stringer = Many(Many(Check((Char) => 'a' <= Char && Char <= 'z') | Many(' '))).Then(EndOfSource);

		[Benchmark]
		public void MSRegex_Good() => msregex.Match(goodGibberish);

		[Benchmark]
		public void MSRegex_Bad() => msregex.Match(badGibberish);

		[Benchmark]
		public void PcreRegex_Good() => pcreregex.Match(goodGibberish);

		[Benchmark]
		public void PcreRegex_Bad() => pcreregex.Match(badGibberish);

		[Benchmark]
		public void FParsec_Good() => fparsec.Gibberish.RunOnString(goodGibberish);

		[Benchmark]
		public void FParsec_Bad() => fparsec.Gibberish.RunOnString(badGibberish);

		[Benchmark]
		public void Pidgin_Good() => pidgin.Parse(goodGibberish);

		[Benchmark]
		public void Pidgin_Bad() => pidgin.Parse(badGibberish);

		[Benchmark]
		public void Sprache_Good() => sprache.Parse(goodGibberish);

		[Benchmark]
		public void Sprache_Bad() {
			try {
				_ = sprache.Parse(badGibberish);
			} catch {
				// We have to do this or benchmarks are thrown out. This is considered part of Sprache's failure mode.
			}
		}

		[Benchmark]
		public void Stringier_Good() => stringer.Consume(goodGibberish);

		[Benchmark]
		public void Stringer_Bad() => stringer.Consume(badGibberish);
	}
}
