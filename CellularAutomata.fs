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

    let empty width height = {
      width = width;
      height = height;
      alive = Set.empty
    }

    let get x y b =
      b.alive |> Set.contains (x, y)

    let set x y v b =
      if isOnBoard x y b then
        let op = if v then Set.add else Set.remove
        { b with alive = op (x, y) b.alive }
      else
        b

    let show b =
      let showCell x y =
        if b.alive |> Set.contains (x, y) then
          "█"
        else
          "░"
      let showRow y = seq { for x in 0I .. b.width - 1I -> showCell x y } |>
                      Seq.fold (+) ""
      seq { for y in 0I .. b.height - 1I -> showRow y + "\n" } |>
        Seq.fold (+) ""

    // Convenience function to get all the neighbours of a cell
    let neighbours x y b =
      Set.empty |>
      Set.add (x - 1I, y - 1I) |>
      Set.add (x, y - 1I) |>
      Set.add (x + 1I, y - 1I) |>
      Set.add (x - 1I, y) |>
      Set.add (x + 1I, y) |>
      Set.add (x - 1I, y + 1I) |>
      Set.add (x, y + 1I) |>
      Set.add (x + 1I, y + 1I) |>
      Set.filter (fun (x, y) -> isOnBoard x y b)

    let next b =

      // Computes the next state of a given cell.
      let cellNext x y =
        let countLivingNeighbours x y =
          neighbours x y b |>
          Set.intersect b.alive |>
          Set.count
        match (get x y b, countLivingNeighbours x y) with
          | (true, 2)
          | (true, 3) -> true
          | (true, _) -> false
          | (false, 3) -> true
          | (false, _) -> false

      // For efficiency reasons, we do not want to update every cell.
      // We only need to update every live cell and its neighbours.
      let cellsToCheck =
        b.alive |>
        Set.map (fun (x, y) -> (neighbours x y b) |> Set.add (x, y)) |>
        Set.fold (+) Set.empty

      // Function from the coordinates of a cell to the transform required
      // to update the board.
      let transform x y =
        fun b -> set x y (cellNext x y) b

      cellsToCheck |>
        Set.toSeq |>
        Seq.map (fun (x, y) -> transform x y) |>
        Seq.fold (fun s f -> f s) b
