using System;
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
	public class OptorBenchmarks {
		public static readonly Regex msregex = new Regex("^(?:Hello)?");

		public static readonly Regex msregexCompiled = new Regex("^(?:Hello)?", RegexOptions.Compiled);

		public static readonly PcreRegex pcreregex = new PcreRegex("^(?:Hello)?");

		public static readonly PcreRegex pcreregexCompiled = new PcreRegex("^(?:Hello)?", PcreOptions.Compiled);

		public static readonly Parser<Char, String> pidgin = Parser.Try(Parser.String("Hello"));

		public static readonly Sprache.Parser<IOption<String>> sprache = Parse.Optional(Parse.String("Hello").Text());

		public static readonly Pattern stringier = Maybe("Hello");

		[Params("Hello", "World")]
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
		public void FParsec() => fparsec.Optor.RunOnString(Source);

		[Benchmark]
		public void Pidgin() => pidgin.Parse(Source);

		[Benchmark]
		public void Sprache() => sprache.Parse(Source);

		[Benchmark]
		public void Stringier() => stringier.Consume(Source);
	}
}
