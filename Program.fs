open System
open Conway
open System.Numerics

[<EntryPoint>]
let main argv =
    let x = CellularAutomata.empty 9I 9I |>
            CellularAutomata.set 0I 0I true |>
            CellularAutomata.set 1I 0I true |>
            CellularAutomata.set 3I 2I true |>
            CellularAutomata.set 5I 6I true
    x |> CellularAutomata.show |> Console.WriteLine
    0
