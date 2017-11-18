open System
open Conway
open System.Numerics

[<EntryPoint>]
let main argv =
    Console.WriteLine "Hello World"
    let x = CellularAutomata.empty 3I 3I |>
            CellularAutomata.set 1I 2I true |>
            CellularAutomata.get 1I 2I |>
            Console.WriteLine
    0
