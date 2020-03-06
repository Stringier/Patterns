namespace Patterns

open System
open Stringier.Patterns
open Defender
open Xunit

type LiteralTests() =
    inherit Trial()

    [<Fact>]
    member _.``consume`` () =
        p"Hello"
        |> consumes "Hello" "Hello World!"
        |> fails "Hell"
        |> fails "Bacon"

    [<Fact>]
    member _.``consume case-insensitive`` () =
        "Hello"/=Compare.CaseInsensitive >> ' '/=Compare.CaseInsensitive >> "World"/=Compare.CaseInsensitive
        |> consumes "HELLO WORLD" "HELLO WORLD"
        |> consumes "hello world" "hello world"
        |> ignore

    [<Fact>]
    member _.``neglect`` () =
        not "Hello"
        |> consumes "Oh no" "Oh no!"
        |> consumes "Oh no" "Oh no?"
        |> fails "Hello"
        |> fails "Hello!"
        |> fails "Hello."
        |> ignore