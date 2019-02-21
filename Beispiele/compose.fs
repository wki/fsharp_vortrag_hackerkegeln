let inc x = x + 1
let double x = x * 2
let square x = x * x
let toString x = sprintf "%d"

let incDoubleQuare = inc >> double >> square

let incStr = inc >> toString


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

let x = maybe {
    let! pos = posInt 42
    let! lt100 = mustBeLt100 pos
    return square lt100
}

printfn "x = %A" x

let posToStr = posInt >> mustBeLt100 >> square

