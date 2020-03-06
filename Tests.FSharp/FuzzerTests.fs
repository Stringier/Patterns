namespace Patterns

open System
open Stringier.Patterns
open Defender
open Xunit

type FuzzerTests() =
    inherit Trial()

    [<Fact>]
    member _.``consume`` () =
        fuzzy "bob"
        |> consumes "bob" "bob"
        |> consumes "mob" "mob"
        |> fails "mom"
        |> ignore