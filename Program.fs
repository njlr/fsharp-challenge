open System
open Conway
open System.Numerics

[<EntryPoint>]
let main argv =
    let x = CellularAutomata.empty 9I 9I |>
            CellularAutomata.set 1I 1I true |>
            CellularAutomata.set 2I 1I true |>
            CellularAutomata.set 3I 1I true |>
            CellularAutomata.set 8I 1I true |>
            CellularAutomata.set 3I 4I true |>
            CellularAutomata.set 5I 7I true |>
            CellularAutomata.next
    x |> CellularAutomata.show |> Console.WriteLine
    0
