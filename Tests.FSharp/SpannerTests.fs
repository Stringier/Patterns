namespace Patterns

open System
open Stringier.Patterns
open Defender
open Xunit

type SpannerTests() =
    inherit Trial()

    [<Fact>]
    let ``consume`` () =
        many ' '
        |> consumes " " " Hi!"
        |> consumes "    " "    Hi!"
        |> fails "Hi!  "

    [<Fact>]
    let ``neglect`` () =
        not (many ';')
        |> consumes "123456789" "123456789;"
        |> fails ";"