
let inc x = x + 1
let double x = x * 2
let square x = x * x
let toString x = sprintf "%d" x

// incDoubleSquare x = square(double(inc(x)))
let incDoubleSquare = inc >> double >> square
let incStr = inc >> toString

// ((5+1) * 2) ^ 2 -> 144
let x = incDoubleSquare 5

let incDoubleSquare2 x =
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

let posToStr = posInt >> mustBeLt100 >> square


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


