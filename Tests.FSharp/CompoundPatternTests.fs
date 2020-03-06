namespace Patterns

open System
open Stringier.Patterns
open Defender
open Xunit

type CompoundPatternTests() =
    inherit Trial()

    [<Fact>]
    let ``alternate repeater`` () =
        ("Hi " || "Bye ") * 2
        |> consumes "Hi Bye " "Hi Bye Hi"
        |> ignore

    [<Fact>]
    let ``alternate spanner`` () =
        many (Pattern.SpaceSeparator || "\t")
        |> consumes "  \t " "  \t "
        |> ignore

    [<Fact>]
    let ``double negator`` () =
        not (not "Hello")
        |> consumes "Hello" "Hello"
        |> ignore

    [<Fact>]
    let ``double optor`` () =
        maybe (maybe "Hello")
        |> consumes "Hello" "Hello"
        |> consumes "" "World"
        |> ignore

    [<Fact>]
    let ``double spanner`` () =
        many (many "hi")
        |> consumes "hi" "hi"
        |> consumes "hihi" "hihi"
        |> consumes "hihihi" "hihihi"
        |> ignore

    [<Fact>]
    let ``optional spanner`` () =
        kleene ' '
        |> consumes "  " "  Hello"
        |> consumes "" "Hello"
        |> ignore

    [<Fact>]
    let ``spaning optor`` () =
        Assert.Throws<PatternConstructionException>(Action (fun () -> many(maybe ' ') |> ignore)) |> ignore