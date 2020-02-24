using System;
using BenchmarkDotNet.Running;
using Consolator.UI;
using Consolator.UI.Theming;
using Console = Consolator.Console;

namespace Benchmarks {
	public static class Program {
		internal readonly static KeyChoiceSet MenuChoices = new KeyChoiceSet(" Enter Choice: ",
			new KeyChoice(ConsoleKey.D1, "Literal", () => BenchmarkRunner.Run<LiteralBenchmarks>()),
			new KeyChoice(ConsoleKey.D2, "Alternator", () => BenchmarkRunner.Run<AlternatorBenchmarks>()),
			new KeyChoice(ConsoleKey.D3, "Concatenator", () => BenchmarkRunner.Run<ConcatenatorBenchmarks>()),
			new KeyChoice(ConsoleKey.D4, "Kleene's Closure", () => BenchmarkRunner.Run<KleeneClosureBenchmarks>()),
			new KeyChoice(ConsoleKey.D5, "Fuzzer", () => BenchmarkRunner.Run<FuzzerBenchmarks>()),
			new KeyChoice(ConsoleKey.D6, "Negator", () => BenchmarkRunner.Run<NegatorBenchmarks>()),
			new KeyChoice(ConsoleKey.D7, "Optor", () => BenchmarkRunner.Run<OptorBenchmarks>()),
			new KeyChoice(ConsoleKey.D8, "Ranger", () => BenchmarkRunner.Run<RangerBenchmarks>()),
			new KeyChoice(ConsoleKey.D9, "Repeater", () => BenchmarkRunner.Run<RepeaterBenchmarks>()),
			new KeyChoice(ConsoleKey.A, "Spanner", () => BenchmarkRunner.Run<SpannerBenchmarks>()),
			new KeyChoice(ConsoleKey.B, "Identifier", () => BenchmarkRunner.Run<IdentifierBenchmarks>()),
			new KeyChoice(ConsoleKey.C, "Checker", () => BenchmarkRunner.Run<CheckerBenchmarks>()),
			new KeyChoice(ConsoleKey.D, "IPv4 Address", () => BenchmarkRunner.Run<IPv4AddressBenchmarks>()),
			new KeyChoice(ConsoleKey.E, "LineComment", () => BenchmarkRunner.Run<LineCommentBenchmarks>()),
			new KeyChoice(ConsoleKey.F, "Phone Number", () => BenchmarkRunner.Run<PhoneNumberBenchmarks>()),
			new KeyChoice(ConsoleKey.G, "String Literal", () => BenchmarkRunner.Run<StringLiteralBenchmarks>()),
			new BackKeyChoice(ConsoleKey.Q, "Quit", () => Environment.Exit(0)));

		public static void Main() {
			Theme.DefaultDark.Apply();

			while (true) {
				Console.WriteChoices(MenuChoices);
				Console.ReadChoice(MenuChoices);
			}
		}
	}
}
