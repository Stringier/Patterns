namespace Patterns

open System
open Stringier.Patterns
open Defender
open Xunit

type ConcatenatorTests() =
    inherit Trial()

    [<Fact>]
    member _.``consume`` () =
        "Hello" >> ' ' >> "world"
        |> consumes "Hello world" "Hello world"
        |> fails "Hello everyone"
        |> ignore

        "Goodbye" >> ' ' >> "world"
        |> consumes "Goodbye world" "Goodbye world"
        |> fails "Hello world"
        |> ignore

    [<Fact>]
    member _.``forward composition still works`` () =
        let f x = x + 1
        let g x = x * 2
        let h = f >> g
        h 1 |> isEqual 4 |> ignore

    [<Fact>]
    member _.``neglect`` () =
        not ("Hello" >> '!')
        |> consumes "Hello." "Hello."
        |> consumes "Oh no!" "Oh no!"
        |> consumes "Oh no?" "Oh no?"
        |> fails "Hello"
        |> fails "Hello!"
        |> ignore