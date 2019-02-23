
let rec parse list =
    match list with
    | [] -> 
        printfn "done"
    | "-n" :: xs -> 
        printfn "dryrun"
        parse xs
    | "-d" :: d :: xs -> 
        printfn "dir: '%s'" d
        parse xs
    | _ ->
        printfn "unrecognized: %A" list

// dir: '/path/to/dirr'
// dryrun
// unrecognized: ["-x"]
parse ["-d"; "/path/to/dir"; "-n"; "-x"]

