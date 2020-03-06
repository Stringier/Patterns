namespace Patterns

open System
open Stringier.Patterns
open Defender
open Xunit

type RepeaterTests() =
    inherit Trial()

    [<Fact>]
    let ``consume`` () =
        "Hi! " * 2
        |> consumes "Hi! Hi! " "Hi! Hi! Hi!"
        |> fails "Bye! Hi! "
        |> ignore

    [<Fact>]
    let ``multiplication still works`` () =
        2 * 2 |> isEqual 4 |> ignore
        3.33 * 2.0 |> isEqual 6.66 |> ignore

    [<Fact>]
    let ``neglect`` () =
        not ("Hi!" * 2)
        |> consumes "Oh!Oh!" "Oh!Oh!"
        |> consumes "Oh!Hi!" "Oh!Hi!"
        |> fails "Hi!Hi!"