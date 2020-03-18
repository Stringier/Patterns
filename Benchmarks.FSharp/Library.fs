namespace Benchmarks

open FParsec

module fparsec =
    let Alternator:Parser<string, obj> = attempt(pstringCI "Hello" <|> pstringCI "Goodbye")

    let Concatenator:Parser<(string * char) * string, obj> = attempt(pstringCI "Hello" .>>. pchar ' ' .>>. pstringCI "World")

    let Literal:Parser<string, obj> = attempt(pstringCI "Hello")

    let Negator:Parser<((((char * char) * char) * char) * char), obj> = attempt (noneOf "H" .>>. noneOf "e" .>>. noneOf "l" .>>. noneOf "l" .>>. noneOf "o")

    let Optor:Parser<string option, obj> = attempt (opt (pstringCI "Hello"))

    let Spanner:Parser<string, obj> = attempt (many1Strings (pstringCI "Hi!"))

    let private word:Parser<string, obj> = many1Chars (anyOf "abcdefghijklmnopqrstuvwxyz")
    let private space:Parser<string, obj> = many1Chars (pchar ' ')
    let Gibberish = many(word <|> space) .>>. eof