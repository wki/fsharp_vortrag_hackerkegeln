let inc x = x + 1
let double x = x * 2
let square x = x * x
let toString x = sprintf "%d" x

let incDoubleQuare = inc >> double >> square
let incStr = inc >> toString

// ((5+1) * 2) ^ 2 -> 144
let x = incDoubleQuare 5

let x2 x =
    x
    |> inc
    |> double
    |> square

let posInt x =
    if x < 0 then
        None
    else
        Some x

let mustBeLt100 x =
    if x < 100 then
        Some x
    else
        None

type MaybeBuilder() =
    member this.Bind(x, f) =
        match x with
        | Some(x) -> f(x)
        | _ -> None
    member this.Return(x) = Some x

let maybe = MaybeBuilder()

[<EntryPoint>]
let main argv =
    let x = maybe {
        let! pos = posInt 42
        let! lt100 = mustBeLt100 (pos + 1)
        return square lt100
    }

    printfn "x = %A, isNone: %A" x (Option.isNone x)

    0 // return an integer exit code
