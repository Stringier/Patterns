namespace Patterns

open System
open Stringier.Patterns
open Defender
open Xunit

type RangerTests() =
    inherit Trial()

    [<Fact>]
    member _.``range consume`` () =
        range "Hello" ';'
        |> consumes "Hello;" "Hello;"
        |> consumes "Hello World;" "Hello World;"
        |> ignore

    [<Fact>]
    member _.``erange consume``() =
        erange "\"" "\"" "\\\""
        |> consumes "\"\"" "\"\""
        |> consumes "\"H\"" "\"H\""
        |> consumes "\"Hello\"" "\"Hello\""
        |> consumes "\"Hello\\\"Goodbye\"" "\"Hello\\\"Goodbye\""
        |> ignore

    [<Fact>]
    member _.``nrange consume`` () =
        nrange "if" "end if"
        |> consumes "if\nif\nend if\nbacon\nend if" "if\nif\nend if\nbacon\nend if\nfoobar"
        |> fails "if\nif\nend if\nbacon\nfoobar"
        |> ignore