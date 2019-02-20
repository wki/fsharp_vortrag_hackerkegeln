type NetAddress =
    | IpAddress of byte * byte * byte * byte
    | DnsAddress of string

let home = IpAddress(127uy, 0uy, 0uy, 1uy)

let print net =
    match net with
    | IpAddress(a,b,c,d) -> printfn "IP %d.%d.%d.%d" a b c d
    | DnsAddress name -> printfn "Dns %s" name

// ------

type Color = Red | Green | Blue

let describe color =
    match color with
    | Red -> "du magst rot"
    | Green -> "ach ja, grün"
 // | Blue -> "blau"

