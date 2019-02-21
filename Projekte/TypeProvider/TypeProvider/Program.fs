open System
open FSharp.Data

type ToDoList = CsvProvider<"sample.csv", ";">

[<EntryPoint>]
let main argv =
    let text = """Id;Status;Message;Todo Comment;Number
    1;todo;"Buy Milk";"needs to be done urgently";42
    2;done;Butter;"maybe";
    3;todo;"Bier";"needs to be done urgently";42
    4;todo;Wasser;"needs to be done urgently";42
    5;todo;Eier;"needs to be done urgently";42"""

    let todos =
        ToDoList.Parse(text)

    todos.Rows
        |> Seq.filter (fun r -> r.Status = "todo")
        |> Seq.iter (fun r -> printfn "TODO #%d: '%s'" r.Id r.Message)

    // Console.ReadLine() |> ignore

    0 // return an integer exit code
