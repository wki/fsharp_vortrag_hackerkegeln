open System.Text.RegularExpressions

type Gender =
    | Men
    | Women
    | Unisex
    | Unknown

type Category =
    | Football
    | Baseball
    | Tennis
    | Other
    | XXXunknown

// active pattern
let (|Int|_|) str =
    match System.Int32.TryParse(str) with
    | (true, int) -> Some(int)
    | _ -> None

// Benutzung des Matchings
let toInt str =
    match str with
    | Int i -> Some i
    | _ -> None

// String Matching
let toCategory str =
    match str with
    | "Fussball"
    | "Football" -> Football
    | "Baseball" -> Baseball
    | "Tenis"
    | "Tennis" -> Tennis
    | _ -> Other

let toGender str =
    match str with
    | "M" 
    | "Men" -> Men
    | "W" 
    | "Women" -> Women
    | _ -> Unisex

// // regex replace helper
// let replace pattern replacement input = Regex.Replace(input, pattern, replacement=replacement)
// let remove regex input = replace regex "" input

// Typisches Muster:
// 12_M_Football_ignoriere dich.txt

type File = { category: Category; gender: Gender; lineNo: int option; name: string option }

// rekursiv stückchenweise von vorne lesen
// und dabei eine File Struktur füllen
let rec toFilePartial fileName resultFile =
    // match and consume a regex, give back either None or Some(match, rest)
    let consumes regex input =
        let m = Regex.Match(input, regex)
        if (m.Success)
        then
            Some (m.Groups.[1].Value, m.Groups.[2].Value)
        else
            None
    
    let (|LineNo|_|)       = consumes @"^Z?0*(\d+)[-_]+(.*)"
    let (|CategoryWord|_|) = consumes @"^([A-Za-z]+)_+(.*)"
    let (|GenderName|_|)   = consumes @"^(M|W|Men|Women)[-_](.*)"
    match fileName with
    | LineNo(line,rest) when Option.isNone resultFile.lineNo ->
        toFilePartial rest { resultFile with lineNo = toInt line }
    | GenderName(g,rest) ->
        toFilePartial rest { resultFile with gender = toGender g }
    | CategoryWord(cat,rest) when toCategory cat <> XXXunknown ->
        toFilePartial rest { resultFile with category = toCategory cat }
    | rest -> { resultFile with name = Some rest }

// aus einer Datei in einem JobFolders eine File Struktur erzeugen
let toFile file = 
    let startResult = {
        category = XXXunknown
        gender = Unknown
        lineNo = None
        name = None
    }
    let resultFile = toFilePartial file startResult

    printfn "File: %A" resultFile

[<EntryPoint>]
let main argv =
    toFile "12_M_Football_blablubb.txt"

    0
