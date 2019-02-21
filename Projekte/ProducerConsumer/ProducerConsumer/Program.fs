open System
open System.Diagnostics
open System.IO

[<AutoOpen>]
module Logger =
    type Info =
        | Starting
        | Received of string
        | Processed of string
        | ShowReport

    let printWithTimestamp (s: string) =
        let now = DateTime.Now.ToString "yyyy-MM-dd HH:mm:ss"
        printfn "%s: %s" now s

    let printDebug format = 
        Printf.kprintf printWithTimestamp format

    let printInfo format =
        Printf.kprintf printWithTimestamp format

    let logger =
        MailboxProcessor<Info>.Start(fun inbox ->
            let rec loop received processed = async {
                let! msg = inbox.Receive()

                match msg with
                | Starting -> 
                    printInfo "Logger starting"
                    return! loop received processed

                | Received s ->
                    if (received % 10) = 0 then
                        printDebug "Received %d, processed %d" received processed
                    return! loop (received + 1) processed

                | Processed s ->
                    if (processed % 10) = 0 then
                        printDebug "Received %d, processed %d" received processed
                    return! loop received (processed + 1)

                | ShowReport ->
                    printInfo "Status: %d received, %d processed" received processed
                    return! loop 0 0
            }

            loop 0 0
        )

[<AutoOpen>]
module Processor =
    let handle =
        MailboxProcessor<string>.Start(fun inbox ->
            let rec loop() = async {
                let! path = inbox.Receive()
                Processed path |> logger.Post

                return! loop() 
            }
    
            loop()
        )

[<AutoOpen>]
module Timer =
    let elapsedHandler (args:Timers.ElapsedEventArgs) =
        ShowReport |> logger.Post

    let tick = new Timers.Timer(3600000.)
    tick.Elapsed.Add(elapsedHandler)
    tick.Start()

[<EntryPoint>]
let main argv =


    1 // fswatch runs forever. Otherwise our stop is abnormal.