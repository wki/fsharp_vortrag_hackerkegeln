type LoggingBuilder() =
    let log p = printfn "expression is %A" p

    member this.Bind(x, f) = 
        log x
        f x

    member this.Return(x) = 
        x

let logger = new LoggingBuilder()

[<EntryPoint>]
let main argv =

    let loggedWorkflow = logger {
        let! x = 42
        let! y = 43
        let! z = x + y
        return z
    }

    printfn "Logged Workflow result = %d" loggedWorkflow

    0 // return an integer exit code
