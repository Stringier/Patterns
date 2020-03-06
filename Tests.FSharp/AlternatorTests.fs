namespace Patterns

open System
open Stringier.Patterns
open Defender
open Xunit

type enum = First = 1
          | Second = 2
          | Third = 3

type AlternatorTests() =
    inherit Trial()

    [<Fact>]
    let ``constructor`` () =
        let _pattern = "Hello" || "Goodbye"
        ()

    [<Fact>]
    let ``consume`` () =
        ("Hello" || "Goodbye")
        |> consumes "Hello" "Hello"
        |> consumes "Goodbye" "Goodbye"
        |> fails "!"
        |> fails "How are you?"
        |> ignore

        ("Hello" || "Hi" || "Howdy")
        |> consumes "Hello" "Hello"
        |> consumes "Hi" "Hi"
        |> consumes "Howdy" "Howdy"
        |> fails "Goodbye"
        |> ignore

        oneOf [|p"Hello";p"Hi";p"Howdy"|]
        |> consumes "Hello" "Hello"
        |> consumes "Hi" "Hi"
        |> consumes "Howdy" "Howdy"
        |> fails "Goodbye"
        |> ignore

        oneOfEnum<enum>
        |> consumes "First" "First"
        |> consumes "Second" "Second"
        |> consumes "Third" "Third"
        |> fails "Fourth"
        |> ignore

    [<Fact>]
    let ``boolean-or still works`` () =
        isTrue (true || true) |> ignore
        isTrue (false || true) |> ignore
        isTrue (true || false) |> ignore
        isFalse (false || false) |> ignore

    [<Fact>]
    let ``neglect`` () =
        not ("Hello" || "Goodbye")
        |> consumes "World" "Worldeater"
        |> fails "Hello"
        |> fails "Goodbye"
        |> ignore