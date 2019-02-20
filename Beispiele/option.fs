type Person = {Name:string; Year:int option}

let p1 = { Name="Wolfgang"; Year=Some 1964 }
let p2 = { Name="Michael"; Year=None}

let alterBerechnen p =
    p.Year
    |> Option.map (fun y -> 2019 - y)

