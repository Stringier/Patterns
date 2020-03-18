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
	public class FuzzerBenchmarks {
		public static readonly Pattern literal = "bob";

		public static readonly Pattern fuzzer = Fuzzy("bob");

		[Params("bob", "mob", "mom")]
		public String Source { get; set; }

		[Benchmark(Baseline = true)]
		public void Literal() => literal.Consume(Source);

		[Benchmark]
		public void Fuzzer() => fuzzer.Consume(Source);
	}
}
