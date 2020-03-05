using System;
using Stringier.Patterns;
using static Stringier.Patterns.Pattern;
using Defender;
using Xunit;

namespace Tests {
	public class StressTests : Trial {
		[Fact]
		public void Gibberish() {
			Source source = new Source(Stringier.Gibberish.Generate(128));
			Pattern letter = Check((c) => 'a' <= c && c <= 'z');
			Pattern word = Many(letter);
			Pattern space = Many(' ');
			Pattern gibberish = Many(word | space).Then(EndOfSource);
			Claim.That(gibberish).SucceedsToConsume(ref source);
		}
	}
}
