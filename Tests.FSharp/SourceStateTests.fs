namespace Patterns

open System
open Stringier.Patterns
open Defender
open Xunit

type SourceStateTests() =
    inherit Trial()

    [<Fact>]
    let ``store and restore`` () =
        let mutable source = Source("hello world")
        let state = source.Store()
        let mutable result = "hello".Consume(&source)
        Assert.True(result.Success)
        source.Restore(state)
        //The only way this can succeed is if the restore worked
        Assert.True(result.Success)