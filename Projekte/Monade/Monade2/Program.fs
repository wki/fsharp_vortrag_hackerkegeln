open System

let split (s:string) =
    match s.Split(',',2) with
    | [|a;b|] -> Some (a,b)
    | _ -> None

let toInt (s:string) =
    match Int32.TryParse s with
    | true, i -> Some i
    | _ -> None

let div a b =
    if b = 0 then
        None
    else
        Some(a/b)
        
type MaybeBuilder() =
    member this.Bind(x, f) =
        match x with
        | Some(x) -> 
            printfn "Bind x=%A" x
            let result = f(x)
            printfn "Bind x=%A, f=%A -> %A" x f result
            result
        | _ -> None
    member this.Return(x) = 
        printfn "Return %d" x
        Some x

let maybe = MaybeBuilder()

[<EntryPoint>]
let main argv =
    let x = maybe {
        let! s1,s2 = split "100,5"
        let! a = toInt s1
        let! b = toInt s2
        let! quot = div a b
        
        return quot
    }

    printfn "result = %A" x
    
    0 // return an integer exit code
