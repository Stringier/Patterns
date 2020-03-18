using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using FParsec.CSharp;
using PCRE;
using Pidgin;
using Sprache;
using Stringier.Patterns;
using static Stringier.Patterns.Pattern;

namespace Benchmarks {
	[SimpleJob(RuntimeMoniker.NetCoreApp31)]
	[SimpleJob(RuntimeMoniker.CoreRt31)]
	[SimpleJob(RuntimeMoniker.Mono)]
	[MemoryDiagnoser]
	public class SpannerBenchmarks {
		public static readonly Regex msregex = new Regex("^(?:Hi!)+");

		public static readonly Regex msregexCompiled = new Regex("^(?:Hi!)+", RegexOptions.Compiled);

		public static readonly PcreRegex pcreregex = new PcreRegex("^(?:Hi!)+");

		public static readonly PcreRegex pcreregexCompiled = new PcreRegex("^(?:Hi!)+", PcreOptions.Compiled);

		public static readonly Parser<Char, String> pidgin = Parser.String("Hi!").AtLeastOnceString();

		public static readonly Sprache.Parser<IEnumerable<String>> sprache = Parse.String("Hi!").Text().AtLeastOnce();

		public static readonly Pattern stringier = Many("Hi!");

		[Params("Hi!", "HiHi!", "Hi!Hi!Hi!", "Okay?")]
		public String Source { get; set; }

		[Benchmark]
		public void MSRegex() => msregex.Match(Source);

		[Benchmark]
		public void MSRegexCompiled() => msregexCompiled.Match(Source);

		[Benchmark]
		public void PcreRegex() => pcreregex.Match(Source);

		[Benchmark]
		public void PcreRegexCompiled() => pcreregexCompiled.Match(Source);

		[Benchmark]
		public void FParsec() => fparsec.Spanner.RunOnString(Source);

		[Benchmark]
		public void Pidgin() => pidgin.Parse(Source);

		[Benchmark]
		public void Sprache() => sprache.Parse(Source);

		[Benchmark]
		public void Stringier() => stringier.Consume(Source);
	}
}
