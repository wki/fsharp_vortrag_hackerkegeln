let roman = ["I"; "V"; "X"]
let numbers = [1 .. 10]

let evenNumbers = 
    let isEven n =
        n % 2 = 0

    numbers 
    |> List.filter isEven

