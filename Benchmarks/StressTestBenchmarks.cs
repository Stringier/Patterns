using System;
using System.Text.RegularExpressions;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using PCRE;
using Pidgin;
using Sprache;
using Stringier;
using Stringier.Patterns;
using static Stringier.Patterns.Pattern;

namespace Benchmarks {
	[SimpleJob(RuntimeMoniker.NetCoreApp31)]
	[SimpleJob(RuntimeMoniker.CoreRt31)]
	[SimpleJob(RuntimeMoniker.Mono)]
	[MemoryDiagnoser]
	public class StressTestBenchmarks {
		public static String goodGibberish = Gibberish.Generate(4);

		public static String badGibberish = Gibberish.Generate(4, bad: true);

		private static readonly Regex msregex = new Regex(@"(?:[a-z]+| +)+$", RegexOptions.Singleline);

		private static readonly PcreRegex pcreregex = new PcreRegex(@"(?:[a-z]+| +)+$", PcreOptions.Singleline);

		private static readonly Parser<Char, String> pidgin = Parser.ManyString(Parser.Letter.Or(Parser.Char(' ')));

		private static readonly Sprache.Parser<String> sprache = Parse.Letter.Or(Parse.Char(' ')).Many().Text();

		private static readonly Pattern stringer = Many(Many(Check((Char) => 'a' <= Char && Char <= 'z') | Many(' ').Then(EndOfSource)));

		[Benchmark]
		public void MSRegex_Good() => msregex.Match(goodGibberish);

		[Benchmark]
		public void MSRegex_Bad() => msregex.Match(badGibberish);

		[Benchmark]
		public void PcreRegex_Good() => pcreregex.Match(goodGibberish);

		[Benchmark]
		public void PcreRegex_Bad() => pcreregex.Match(badGibberish);

		[Benchmark]
		public void Pidgin_Good() => pidgin.Parse(goodGibberish);

		[Benchmark]
		public void Pidgin_Bad() => pidgin.Parse(badGibberish);

		[Benchmark]
		public void Sprache_Good() => sprache.Parse(goodGibberish);

		[Benchmark]
		public void Sprache_Bad() => sprache.Parse(badGibberish);

		[Benchmark]
		public void Stringier_Good() => stringer.Consume(goodGibberish);

		[Benchmark]
		public void Stringer_Bad() => stringer.Consume(badGibberish);
	}
}
