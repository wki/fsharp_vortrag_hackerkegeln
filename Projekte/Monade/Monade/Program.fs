let inc x = x + 1
let double x = x * 2
let square x = 
    printfn "square %d" x
    x * x
let toString x = sprintf "%d" x

let incDoubleQuare = inc >> double >> square
let incStr = inc >> toString

// ((5+1) * 2) ^ 2 -> 144
// let x = incDoubleQuare 5

let x2 x =
    x
    |> inc
    |> double
    |> square

let posInt x =
    printfn "posint %d" x
    if x < 0 then
        None
    else
        Some (x+1)

let mustBeLt100 x =
    printfn "mustbelt %d" x
    if x < 100 then
        Some x
    else
        None

type MaybeBuilder() =
    member this.Bind(x, f) =
        match x with
        | Some(x) -> 
            printfn "Bind"
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
        let! pos = posInt 10
        let! lt100 = mustBeLt100 (pos + 1)
        return square lt100
    }

    printfn "x = %A, isNone: %A" x (Option.isNone x)

    0 // return an integer exit code
