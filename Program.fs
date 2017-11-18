open System
open System.Numerics
open Conway

[<EntryPoint>]
let main argv =
    let state = CellularAutomata.empty 16I 16I
    CLI.commandLoop state |> ignore
    0
