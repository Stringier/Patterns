using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Stringier.Patterns;
using static Stringier.Patterns.Pattern;

namespace Benchmarks {
	[SimpleJob(RuntimeMoniker.NetCoreApp31)]
	[SimpleJob(RuntimeMoniker.CoreRt31)]
	[SimpleJob(RuntimeMoniker.Mono)]
	[MemoryDiagnoser]
	public class CheckerBenchmarks {
		public static readonly Pattern alternator = OneOf('0', '1', '2', '3', '4', '5', '6', '7', '8', '9');

		public static readonly Pattern checker = Check((Char) => 0x30 <= Char && Char <= 0x39);

		[Params("0", "1", "a")]
		public String Source { get; set; }

		[Benchmark(Baseline = true)]
		public void Alternator() => alternator.Consume(Source);

		[Benchmark]
		public void Checker() => checker.Consume(Source);
	}
}
