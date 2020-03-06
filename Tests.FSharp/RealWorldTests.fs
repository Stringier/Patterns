namespace Patterns

open System
open Stringier
open Stringier.Patterns
open Defender
open Xunit

type RealWorldTests() =
    inherit Trial()

    [<Fact>]
    member _.``comment`` () =
        Pattern.LineComment("--")
        |> consumes "--This is a comment" "--This is a comment\n"
        |> consumes "--This is a comment" "--This is a comment\nExample_Function();"

    [<Fact>]
    member _.``identifier`` () =
        Pattern.Check((fun (char:char) -> char.IsLetter()), true,
                      (fun (char:char) -> char.IsLetter() || char = '_'), true,
                      (fun (char:char) -> char.IsLetter() || char = '_'), false)
        |> consumes "hello" "hello"
        |> consumes "example_name" "example_name"
        |> fails "_fail"

    [<Fact>]
    member _.``IPv4 address`` () =
        let digit = Pattern.Check((fun (char:char) -> '0' <= char && char <= '2'), false,
                                  (fun (char:char) -> '0' <= char && char <= '9'), false,
                                  (fun (char:char) -> '0' <= char && char <= '9'), true)
        digit
        |> consumes "1" "1"
        |> consumes "11" "11"
        |> consumes "111" "111"
        |> ignore

        digit >> '.' >> digit >> '.' >> digit >> '.' >> digit
        |> consumes "192.168.1.1" "192.168.1.1"
        |> ignore

    [<Fact>]
    member _.``named statement`` () =
        let identifier = Pattern.Check(Bias.Head,
                                      (fun (char:char) -> char.IsLetter() || char = '_'),
                                      (fun (char:char) -> char.IsLetterOrDigit() || char = '_'),
                                      (fun (char:char) -> char.IsLetterOrDigit()))
        identifier
        |> consumes "Name" "Name"
        |> ignore

        let mutable capture = ref null
        "statement" >> many Pattern.Separator >> (identifier => capture)
        |> consumes "statement Name" "statement Name"
        |> ignore

        capture
        |> captures "Name"
        |> ignore

    [<Fact>]
    member _.``phone number`` () =
        Pattern.Number * 3 >> '-' >> Pattern.Number * 3 >> '-' >> Pattern.Number * 4
        |> consumes "555-555-5555" "555-555-5555"
        |> ignore

    [<Fact>]
    member _.``read until end of line`` () =
        many (not (';' || Pattern.LineTerminator))
        |> consumes "hello" "hello"
        |> ignore

    [<Fact>]
    member _.``string literal`` () =
        Pattern.StringLiteral("\"", "\\\"")
        |> consumes "\"hello\\\"world\"" "\"hello\\\"world\""
        |> ignore

    [<Fact>]
    member _.``web address`` () =
        let protocol = "http" >> maybe 's' >> "://"
        let host = many(Pattern.Letter || Pattern.Number || '-') >> '.' >> (Pattern.Letter * 3 >> Pattern.EndOfSource || many(Pattern.Letter || Pattern.Number || '-') >> '.' >> Pattern.Letter * 3)
        let location = many('/' >> many(Pattern.Letter || Pattern.Number || '-' || '_'))
        let address = maybe protocol >> host >> maybe location

        protocol
        |> consumes "http://" "http://"
        |> consumes "https://" "https://"
        |> ignore

        host
        |> consumes "google.com" "google.com"
        |> consumes "www.google.com" "www.google.com"
        |> ignore

        location
        |> consumes "/about" "/about"
        |> ignore

        address
        |> consumes "http://www.google.com" "http://www.google.com"
        |> consumes "https://www.google.com/about" "https://www.google.com/about"
        |> ignore