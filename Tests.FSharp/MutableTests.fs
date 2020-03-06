namespace Patterns

open System
open Stringier.Patterns
open Defender
open Xunit

type MutableTests() =
    inherit Trial()

    [<Fact>]
    let ``mutate and consume`` () =
        let pattern = Pattern.Mutable()
        pattern >> "hello" >> ' ' >> "world"
        //If you check the pattern at this point, you'll see it's as you would expect.
        |> consumes "hello world" "hello world!"
        |> ignore

        seal (not pattern)
        //If you check the pattern at this point, you'll see it was negated. In a non-mutable context a new pattern would have been created here, and returned here, that was left unassigned. Instead the already assigned pattern has been mutated.
        |> consumes "bacon cakes" "bacon cakes!"
        |> ignore

        pattern.Then('!')
        //Since the pattern has been made ReadOnly (Sealed), this call works as it would normally, and if you check at this point, will be a normal pattern who's head has been string concatenated. I've created a new let-binding so that the previous one still exists (the debugger still sees it), and you can see that the previous pattern stays the same.
        |> consumes "bacon cakes!" "bacon cakes!"
        |> ignore

    [<Fact>]
    let ``right-recursion`` () =
        let pattern = Pattern.Mutable()
        pattern >> "hi!" >> (Pattern.EndOfSource || pattern) |> seal
        |> consumes "hi!" "hi!"
        |> consumes "hi!hi!" "hi!hi!"
        |> consumes "hi!hi!hi!" "hi!hi!hi!"
        |> consumes "hi!hi!hi!hi!" "hi!hi!hi!hi!"

    [<Fact>]
    let ``mutual-recursion`` () =
        //These grammar rules aren't great, and can easily be broken, so don't use them as a reference. They just show that mutual recursion is working.
        let expression = Pattern.Mutable()
        let parenExpression = '(' >> expression >> ')'
        let term = Pattern.DecimalDigitNumber
        let factor = (term || parenExpression) >> ("+" || "-") >> (term || parenExpression)
        expression >> (factor || term || parenExpression) |> seal
        |> consumes "3+2" "3+2"
        |> consumes "3+(1+2)" "3+(1+2)"
        |> consumes "(1+2)+3" "(1+2)+3"
        |> consumes "(1+2)-(3+4)" "(1+2)-(3+4)"
        |> consumes "(1+(2+3))-4" "(1+(2+3))-4"
        |> ignore
