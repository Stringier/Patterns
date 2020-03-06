namespace Patterns

open System
open Stringier
open Stringier.Patterns
open Defender
open Xunit

type StressTests() =
    inherit Trial()

    [<Fact>]
    let ``gibberish`` () =
        let source = Gibberish.Generate(128)
        let letter = Pattern.Check((fun (char) -> 'a' <= char && char <= 'z'))
        let word = many letter
        let space = many ' '
        (many (word || space)) >> Pattern.EndOfSource
        |> succeeds source