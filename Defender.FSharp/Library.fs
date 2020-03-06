namespace Defender

open System
open Stringier.Patterns
open Xunit.Sdk

[<AutoOpen>]
module Claims =
    let captures (exp:String) (cap:Capture ref) =
        if cap.Value.ToString() <> exp then
            raise(EqualException(exp, cap.Value.ToString()))
        cap

    let consumes (exp:String) (src:String) (ptn:Pattern) =
        let result = ptn.Consume(src)
        if result.ToString() <> exp then
            raise(EqualException(exp, result.ToString()))
        ptn

    let fails (src:String) (ptn:Pattern) =
        let result = ptn.Consume(src)
        if result.Success then
            raise(FalseException("Parser succeeded when expected to fail", Nullable(result.Success)))
        ptn
    
    let succeeds (src:String) (ptn:Pattern) =
        let result = ptn.Consume(src)
        if not(result.Success) then
            raise(TrueException("Parser failed when expected to succeed", Nullable(result.Success)))
        ptn

