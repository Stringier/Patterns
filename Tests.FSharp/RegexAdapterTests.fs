namespace Patterns

open System
open System.Text.RegularExpressions
open Stringier.Patterns
open Defender
open Xunit

type RegexAdapterTests() =
    inherit Trial()

    [<Fact>]
    member _.``converter`` () =
        let mutable pattern = (fun () -> Regex(@"hello").AsPattern() |> ignore)
        Assert.Throws<PatternConstructionException>(Action pattern) |> ignore
        Regex(@"^hello").AsPattern() |> ignore
        pattern <- (fun () -> Regex(@"hello$").AsPattern() |> ignore)
        Assert.Throws<PatternConstructionException>(Action pattern) |> ignore
        Regex(@"^hello$").AsPattern() |> ignore
        
    [<Fact>]
    member _.``consume`` () =
        p(Regex(@"^hello"))
        |> consumes "hello" "hello"
        |> consumes "hello" "hello world"
        |> fails "hel"
        |> ignore