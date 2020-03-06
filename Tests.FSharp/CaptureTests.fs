namespace Patterns

open System
open Stringier.Patterns
open Defender
open Xunit

type CaptureTests() =
    inherit Trial()

    [<Fact>]
    let ``constructor`` () =
        let mutable capture = ref null
        let _pattern = ('a' || 'b' || 'c') => capture
        ()

    [<Fact>]
    let ``consume`` () =
        let mutable capture = ref null
        
        ('a' || 'b' || 'c') => capture
        |> consumes "a" "a"   
        |> ignore
        capture |> captures "a" |> ignore

        ('a' || 'b' || 'c') => capture >> '!'
        |> consumes "a!" "a!"
        |> fails "a"
        |> ignore
        capture |> captures "a" |> ignore

        ('a' || 'b' || 'c') => capture >> ',' >> capture
        |> consumes "a,a" "a,a"
        |> consumes "b,b" "b,b"
        |> consumes "c,c" "c,c"
        |> fails "b,a"
        |> ignore