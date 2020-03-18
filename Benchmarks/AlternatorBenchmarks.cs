using System;
using System.Text.RegularExpressions;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using FParsec.CSharp;
using PCRE;
using Pidgin;
using Sprache;
using Stringier.Patterns;

namespace Benchmarks {
	[SimpleJob(RuntimeMoniker.NetCoreApp31)]
	[SimpleJob(RuntimeMoniker.CoreRt31)]
	[SimpleJob(RuntimeMoniker.Mono)]
	[MemoryDiagnoser]
	public class AlternatorBenchmarks {
		public static readonly Regex msregex = new Regex("^(?:Hello|Goodbye)");

		public static readonly Regex msregexCompiled = new Regex("^(?:Hello|Goodbye)", RegexOptions.Compiled);

		public static readonly PcreRegex pcreregex = new PcreRegex("^(?:Hello|Goodbye)");

		public static readonly PcreRegex pcreregexCompiled = new PcreRegex("^(?:Hello|Goodbye)", PcreOptions.Compiled);

		public static readonly Parser<Char, String> pidgin = Parser.String("Hello").Or(Parser.String("Goodbye"));

		public static readonly Sprache.Parser<String> sprache = Parse.String("Hello").Or(Parse.String("Goodbye")).Text();

		public static readonly Pattern stringier = "Hello".Or("Goodbye");

		[Params("Hello", "Goodbye", "Failure")]
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
		public void FParsec() => fparsec.Alternator.RunOnString(Source);

		[Benchmark]
		public void Pidgin() => pidgin.Parse(Source);

		[Benchmark]
		public void Sprache() => sprache.Parse(Source);

		[Benchmark]
		public void Stringier() => stringier.Consume(Source);
	}
}
