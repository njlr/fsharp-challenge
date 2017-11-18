namespace Conway

open System.Numerics

module CellularAutomata =

    type Board = {
      width : bigint;
      height : bigint;
      alive : Set<bigint * bigint>
    }

    let isOnBoard x y b =
      x >= 0I && y >= 0I && x < b.width && y < b.height

    // creates a new cellular automata with all 'dead' cells for the given x and y dimensions
    //val empty: x:bigint -> y:bigint -> CellularAutomata
    let empty width height = {
      width = width;
      height = height;
      alive = Set.empty
    }

    // returns true if the cell at the specified coordinates is 'live'
    //val get: x:bigint -> y:bigint -> CellularAutomata -> bool*/
    let get x y b =
      b.alive |> Set.contains(x, y)

    // returns a cellular automata with the cell set to the specified state at the specified coordinates
    //val set: x:bigint -> y:bigint -> bool -> CellularAutomata -> CellularAutomata*/
    let set x y v b =
      if isOnBoard x y b then
        let op = if v then Set.add else Set.remove
        { b with alive = op (x, y) b.alive }
      else
        b

    // returns a cellular automata that is advanced to the next state
    //val next: CellularAutomata -> CellularAutomata*/
