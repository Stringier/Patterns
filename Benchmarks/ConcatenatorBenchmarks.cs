﻿using System;
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
	public class ConcatenatorBenchmarks {
		public static readonly Regex msregex = new Regex("^Hello World");

		public static readonly Regex msregexCompiled = new Regex("^Hello World", RegexOptions.Compiled);

		public static readonly PcreRegex pcreregex = new PcreRegex("^Hello World");

		public static readonly PcreRegex pcreregexCompiled = new PcreRegex("^Hello World", PcreOptions.Compiled);

		public static readonly Parser<Char, String> pidgin = Parser.Map((first, second, third) => first + second + third, Parser.String("Hello"), Parser.Char(' '), Parser.String("World"));

		public static readonly Sprache.Parser<String> sprache =
			from first in Parse.String("Hello").Text()
			from second in Parse.Char(' ')
			from third in Parse.String("World").Text()
			select first + second + third;

		public static readonly Pattern stringier = "Hello".Then(' ').Then("World");

		[Params("Hello World", "Failure")]
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
		public void FParsec() => fparsec.Concatenator.RunOnString(Source);

		[Benchmark]
		public void Sprache() => sprache.Parse(Source);

		[Benchmark]
		public void Pidgin() => pidgin.Parse(Source);

		[Benchmark]
		public void Stringier() => stringier.Consume(Source);
	}
}
