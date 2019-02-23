open System
open System.Text.RegularExpressions
open Microsoft.FSharp.Reflection

// Matching von Datei-Namen.
// Typisches Muster:
let filename = "12_Football_rest_des_namens.txt"

type Category =
    | Football
    | Baseball
    | Tennis
    | Other
    | XXXunknown

type File = { category: Category; lineNo: int option; name: string option }

// regex matching test
let consumes regex input =
    let m = Regex.Match(input, regex)
    if (m.Success)
    then
        Some (m.Groups.[1].Value, m.Groups.[2].Value)
    else
        None

// Konvertierung String -> Discriminated Union
let fromString<'a> (s:string) =
    match FSharpType.GetUnionCases typeof<'a> |> Array.filter (fun case -> case.Name = s) with
    |[|case|] -> Some(FSharpValue.MakeUnion(case,[||]) :?> 'a)
    |_ -> None

// Active Pattern: exakt passender string -> Category
let (|Cat|_|) = fromString<Category>

// active pattern: string -> int
let (|Int|_|) (str:string) =
    match Int32.TryParse(str) with
    | (true, int) -> Some(int)
    | _ -> None

// parsen eines Files nach Aufteilung in Teile
let parseFile1 (filename:string) =
    let rec partialParseFile parts file =
        match parts with
        | [] -> 
            file
        | Int(i)::rest -> 
            partialParseFile rest {file with lineNo=Some i}
        | Cat(c)::rest -> 
            partialParseFile rest {file with category=c}
        | _ ->
            {file with name=Some(String.Join("_", parts))}

    let parts =
        filename.Split([|'_'|])
        |> Array.toList

    partialParseFile parts {category=XXXunknown; lineNo=None; name=None}

// active Pattern: string Anfang -> int
let (|LineNo|_|) str = 
    match (consumes @"^Z?0*(\d+)[-_]+(.*)" str) with 
    | Some(Int(i), rest) -> Some(i, rest)
    | _ -> None

// Active Pattern: string Anfang -> Category
let (|CategoryWord|_|) str = 
    match (consumes @"^([A-Za-z]+)_+(.*)" str) with
    | Some(Cat(c), rest) -> Some(c, rest)
    | _ -> None

// parsen eines Files aus einem String
let parseFile2 filename =
    let rec toFilePartial fileName file =
        match fileName with
        | LineNo(i,rest) when Option.isNone file.lineNo ->
            toFilePartial rest { file with lineNo = Some i }
        | CategoryWord(c,rest) ->
            toFilePartial rest { file with category = c }
        | rest -> { file with name = Some rest }

    toFilePartial filename {category=XXXunknown; lineNo=None; name=None}

[<EntryPoint>]
let main argv =
    let file1 = parseFile1 filename
    printfn "File = %A" file1

    let file2 = parseFile2 filename
    printfn "File = %A" file2

    Console.ReadLine() |> ignore

    0
