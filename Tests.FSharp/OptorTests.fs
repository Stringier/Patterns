namespace Patterns

open System
open Stringier.Patterns
open Defender
open Xunit

type OptorTests() =
    inherit Trial()

    [<Fact>]
    member _.``consume`` () =
        maybe "Hello"
        |> consumes "Hello" "Hello world!"
        |> consumes "" "Goodbye world!"
        |> ignore

    [<Fact>]
    member _.``neglect`` () =
        not (maybe "Hello")
        |> consumes "" "Hello"
        |> consumes "World" "World"
        |> ignore