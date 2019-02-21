#r "/Users/u1022/.nuget/packages/FSharp.Data/3.0.0/lib/netstandard2.0/FSharp.Data.dll"
// #r "/Users/u1022/.nuget/packages/FSharp.Data/3.0.0/lib/netstandard2.0/FSharp.Data.DesignTime.dll"

open System.IO
open FSharp.Data

type ToDoList = CsvProvider<"sample.csv", ";">

let todos =
ToDoList.Parse(File.ReadAllText("masterlist.csv"))

todoJobs.Rows
    |> Seq.filter (fun r -> r.Auftrag = jobDir)
    |> Seq.iter (fun r -> printfn "TODO #%d: %s" r.Id r.Message)

