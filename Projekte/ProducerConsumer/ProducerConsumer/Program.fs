open System

//
// Log messages in a thread of its own
// needed to avoid competing writes to console
//
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

//
// process string messages (slow)
//
[<AutoOpen>]
module Processor =
    let handle =
        MailboxProcessor<string>.Start(fun inbox ->
            let rec loop() = async {
                let! path = inbox.Receive()
                do! Async.Sleep 100
                Processed path |> logger.Post

                return! loop() 
            }
    
            loop()
        )

//
// trigger a periodical log entry
//
[<AutoOpen>]
module Timer =
    let elapsedHandler (args:Timers.ElapsedEventArgs) =
        ShowReport |> logger.Post

    let tick = new Timers.Timer(60000.)
    tick.Elapsed.Add(elapsedHandler)
    tick.Start()

//
// generate random messages (maybe fast)
//
[<AutoOpen>]
module Producer =
    let random = Random()

    let rec produceOnce() = async {
        do! Async.Sleep (random.Next(100, 10000))

        let count = random.Next(5, 20)
        [1..count]
        |> List.map (fun i -> random.Next(1000, 9999))
        |> List.iter (fun x ->
            let message = sprintf "Secret: %d" x
            message |> handle.Post
            Received message |> logger.Post 
        )
    }

    let rec produce() =
        produceOnce() |> Async.RunSynchronously
        produce()

[<EntryPoint>]
let main argv =
    // indicate start of program
    Starting |> logger.Post

    // runs synchronously, blocks
    produce()

    1 // loop runs forever. Otherwise our stop is abnormal.