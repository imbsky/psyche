﻿open ParserCombinator

type Expr =
    | IntLiteral of value : int
    | Add of lhs : Expr * rhs : Expr

let rec showExpr =
    function
    | IntLiteral(v) ->
        string (v)
    | Add(lhs, rhs) ->
        "Add(" + (showExpr lhs) + ", " + (showExpr rhs) + ")"

[<EntryPoint>]
let main argv =
    let simpleParser = fmap (fun (loc, src) -> (loc, int(src))) (token "123")

    match parse simpleParser (bof, argv.[0]) with
    | Some { ast = ast; currentLoc = loc; rest = rest } ->
        printfn "Success!"
        printfn "parsedTo: %d" <| snd ast
        printfn "currentLoc: %s" <| loc.ToString ()
        printfn "rest: \"%s\"" rest
    | None ->
        printfn "Failed..."
    0